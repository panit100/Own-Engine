using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum TriggerModes
{
    CylindricalSector,
    SphericalSector,
    Spherical,
}

public class TurretTrigger : MonoBehaviour
{   
    public TriggerModes Mode;   

    public Transform target;

    public float innerRadius = 1;
    public float outerRadius = 10f;

    public float height = 1;     
    // [Range(0f, 1f)]
    // public float angThresh = 0.5f; //not an actual

    [Range(0, 180)]
    public float fovDeg = 45;
    float fovRad => fovDeg * Mathf.Deg2Rad;
    float angThresh => Mathf.Cos(fovRad / 2);

    void OnDrawGizmos() 
    {
        if(target == null)
            return;

        //make gizmos relative to this transform
        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;

        switch(Mode)
        {
            case TriggerModes.CylindricalSector:
                DrawCylindricalSector();
                break;
            case TriggerModes.SphericalSector:
                DrawSphericalSector();
                break;
            case TriggerModes.Spherical:
                DrawSpherical();
                break;
        }
    }

    void DrawCylindricalSector()
    {
        Gizmos.color = Handles.color = Contains(target.position) ? Color.red:Color.white;

        Vector3 top = new Vector3(0,height,0);

        float p = angThresh;
        float x = Mathf.Sqrt(1 - p * p);

        Vector3 inVLeft = new Vector3(-x,0,p) * innerRadius;
        Vector3 inVRight = new Vector3(x,0,p) * innerRadius;

        Vector3 outVLeft = new Vector3(-x,0,p) * outerRadius;
        Vector3 outVRight = new Vector3(x,0,p) * outerRadius;

        Handles.DrawWireArc(default,Vector3.up,inVLeft,fovDeg,innerRadius);
        Handles.DrawWireArc(top,Vector3.up,inVLeft,fovDeg,innerRadius);

        Handles.DrawWireArc(default,Vector3.up,outVLeft,fovDeg,outerRadius);
        Handles.DrawWireArc(top,Vector3.up,outVLeft,fovDeg,outerRadius);

        // Handles.DrawWireDisc(default,Vector3.up,range);
        // Handles.DrawWireDisc(top,Vector3.up,range);

        Gizmos.DrawLine(inVRight,outVRight);
        Gizmos.DrawLine(inVLeft,outVLeft);

        Gizmos.DrawLine(top + inVRight,top + outVRight);
        Gizmos.DrawLine(top + inVLeft,top + outVLeft);

        
        Gizmos.DrawLine(inVRight,inVRight + top);
        Gizmos.DrawLine(inVLeft,inVLeft + top);

        Gizmos.DrawLine(outVRight,top+outVRight);
        Gizmos.DrawLine(outVLeft,top+outVLeft);
    }

    void DrawSphericalSector()
    {
        Gizmos.color = Handles.color = Contains(target.position) ? Color.red:Color.white;
        
        Gizmos.DrawWireSphere(default,innerRadius);
        Gizmos.DrawWireSphere(default,outerRadius);
    }

    void DrawSpherical()
    {
        Gizmos.color = Handles.color = Contains(target.position) ? Color.red:Color.white;

        float p = angThresh;
        float x = Mathf.Sqrt(1 - p * p);

        Vector3 vLeft = new Vector3(-x,0,p) * outerRadius;
        Vector3 vRight = new Vector3(x,0,p) * outerRadius;
        Vector3 vUp = new Vector3(0,x,p) * outerRadius;
        Vector3 vDown = new Vector3(0,-x,p) * outerRadius;

        float innerRadius = vRight.x;

        Vector3 innerCenter =  new Vector3(0,0,vRight.z) + Vector3.zero;

        Handles.DrawWireDisc(innerCenter,transform.forward,innerRadius);

        Gizmos.DrawLine(default ,vLeft);
        Gizmos.DrawLine(default ,vRight);
        Gizmos.DrawLine(default ,vUp);
        Gizmos.DrawLine(default ,vDown);

        Gizmos.DrawRay(default,Vector3.forward * outerRadius);
        Gizmos.DrawWireSphere(default,outerRadius);

    }

    public bool Contains(Vector3 position)
    {
        switch(Mode)
        {
            case TriggerModes.CylindricalSector:
                return CylindricalSectorContains(position);
            case TriggerModes.SphericalSector:
                return SphericalSectorContains(position);
            case TriggerModes.Spherical:
                return SphericalContains(position);
            default:
                return false;
        }
    }

    public bool CylindricalSectorContains(Vector3 position)
    {
        //Inverse transform world to local
        Vector3 vecToTargetWorld = (position - transform.position);

        Vector3 vecToTarget = transform.InverseTransformVector(vecToTargetWorld);
        Vector3 dirToTarget = vecToTarget.normalized;

        //height position check Optional

        // float y = Vector3.Dot(transform.up,vecToTargetWorld); //Don't have to use Dot function if can use only y 
        // if(y < 0 || y > height)
        //     return false;   
        
        //height position check

        if(vecToTarget.y < 0 || vecToTarget.y > height)
            return false; //outside th height radius

        //angular check

        Vector3 flatDirToTarget = vecToTarget;
        flatDirToTarget.y = 0;
        float flatDistance = flatDirToTarget.magnitude;
        flatDirToTarget /= flatDistance; // normalizes

        if(flatDirToTarget.z < angThresh)
            return false; // outside the angular wedge

        //cylindrical radial

        if(flatDistance > outerRadius || flatDistance < 0 || flatDistance < innerRadius)
            return false; // outside the cylinder

        return true;    
    }

    public bool SphericalSectorContains(Vector3 position)
    {
        Vector3 vecToTargetWorld = position - transform.position;   
        Vector3 vecToTarget = transform.InverseTransformVector(vecToTargetWorld);

        float disToTarget = vecToTarget.magnitude;
        
        if(disToTarget > outerRadius || disToTarget < innerRadius)
            return false;

        return true;
    }

    public bool SphericalContains(Vector3 position)
    {
        Vector3 vecToTargetWorld = (position - transform.position);
        Vector3 vecToTarget = transform.InverseTransformVector(vecToTargetWorld);

        float disToTarget = vecToTarget.magnitude;
        Vector3 dirToTarget = vecToTarget / disToTarget;

        if(dirToTarget.z < angThresh && dirToTarget.x < angThresh)
            return false;

        if(disToTarget > outerRadius)
            return false;   

        return true;
    }
    
#region A
    // void OnDrawGizmos() 
    // {

    //     Vector3 origin = transform.position;    
    //     Vector3 up = transform.up;
    //     Vector3 forward = transform.forward;
    //     Vector3 right = transform.right;

    //     Vector3 top = origin + up * height;

    //     Handles.DrawWireDisc(origin,up,range);
    //     Handles.DrawWireDisc(top,up,range);

    //     float p = angThresh;
    //     float x = Mathf.Sqrt(1 - p * p);

    //     Vector3 vLeft = (forward * p + right * (-x)) * range;
    //     Vector3 vRight = (forward  * p + right * x) * range;

    //     Gizmos.DrawRay(origin,vRight);
    //     Gizmos.DrawRay(origin,vLeft);
    //     Gizmos.DrawRay(top,vRight);
    //     Gizmos.DrawRay(top,vLeft);

    //     Gizmos.DrawLine(origin,top);
    //     Gizmos.DrawLine(origin+vRight,top+vRight);
    //     Gizmos.DrawLine(origin+vLeft,top+vLeft);

        
    // }
#endregion

#region B
    // void OnDrawGizmos() 
    // {
    //     if(target == null)
    //         return;

    //     //make gizmos relative to this transform
    //     Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;

    //     Gizmos.color = Handles.color = Contains(target.position) ? Color.red:Color.white;

    //     Vector3 top = new Vector3(0,height,0);

    //     Handles.DrawWireDisc(default,Vector3.up,range);
    //     Handles.DrawWireDisc(top,Vector3.up,range);

    //     float p = angThresh;
    //     float x = Mathf.Sqrt(1 - p * p);

    //     Vector3 vLeft = new Vector3(-x,0,p) * range;
    //     Vector3 vRight = new Vector3(x,0,p) * range;

    //     Gizmos.DrawRay(default,vRight);
    //     Gizmos.DrawRay(default,vLeft);
    //     Gizmos.DrawRay(top,vRight);
    //     Gizmos.DrawRay(top,vLeft);

    //     Gizmos.DrawLine(default,top);
    //     Gizmos.DrawLine(vRight,top+vRight);
    //     Gizmos.DrawLine(vLeft,top+vLeft);
    // }

    // public bool Contains(Vector3 position)
    // {
    //     //Inverse transform world to local
    //     Vector3 vecToTargetWorld = (position - transform.position);

    //     Vector3 vecToTarget = transform.InverseTransformVector(vecToTargetWorld);
    //     Vector3 dirToTarget = vecToTarget.normalized;

    //     //height position check Optional

    //     // float y = Vector3.Dot(transform.up,vecToTargetWorld); //Don't have to use Dot function if can use only y 
    //     // if(y < 0 || y > height)
    //     //     return false;   
        
    //     //height position check

    //     if(vecToTarget.y < 0 || vecToTarget.y > height)
    //         return false; //outside th height radius


    //     //angular check

    //     Vector3 flatDirToTarget = vecToTarget;
    //     flatDirToTarget.y = 0;
    //     float flatDistance = flatDirToTarget.magnitude;
    //     flatDirToTarget /= flatDistance; // normalizes

    //     if(flatDirToTarget.z < angThresh)
    //         return false; // outside the angular wedge

    //     //cylindrical radial

    //     if(flatDistance > range || flatDistance < 0)
    //         return false; // outside the cylinder

    //     return true;    
    // }

#endregion

}
