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

    void SetGizmoMatrix(Matrix4x4 m) => Gizmos.matrix = Handles.matrix = m;


    void OnDrawGizmos() 
    {
        if(target == null)
            return;

        //make gizmos relative to this transform
        SetGizmoMatrix(transform.localToWorldMatrix);
        Gizmos.color = Handles.color = Contains(target.position) ? Color.red:Color.white;


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

    void DrawSpherical()
    {
        Gizmos.DrawWireSphere(default,innerRadius);
        Gizmos.DrawWireSphere(default,outerRadius);
    }

    void DrawSphericalSector()
    {
        float p = angThresh;
        float x = Mathf.Sqrt(1 - p * p);

        Vector3 vLeftDir = new Vector3(-x,0,p);
        Vector3 vRightDir = new Vector3(x,0,p);

        Vector3 outVLeft = vLeftDir * outerRadius;
        Vector3 outVRight = vRightDir * outerRadius;

        Vector3 inVLeft = vLeftDir * innerRadius;
        Vector3 inVRight = vRightDir * innerRadius;

        float innerCircleRadius = outVRight.x;
        float outerCircleRadius = inVRight.x;

        Vector3 innerCircleCenter =  new Vector3(0,0,outVRight.z) + Vector3.zero;
        Vector3 outerCircleCenter =  new Vector3(0,0,inVRight.z) + Vector3.zero;

        Handles.DrawWireDisc(innerCircleCenter,Vector3.forward,innerCircleRadius);
        Handles.DrawWireDisc(outerCircleCenter,Vector3.forward,outerCircleRadius);

        DrawFlatWedge();
        Matrix4x4 prevMtx = Gizmos.matrix;

        //temporaily modify the matrix
        //draw the rotated gizmo
        SetGizmoMatrix(Gizmos.matrix * Matrix4x4.TRS(default,Quaternion.Euler(0,0,90),Vector3.one));
        DrawFlatWedge();
        SetGizmoMatrix(prevMtx);


        void DrawFlatWedge()
        {
            Handles.DrawWireArc(default,Vector3.up,outVLeft,fovDeg,outerRadius);
            Handles.DrawWireArc(default,Vector3.up,inVLeft,fovDeg,innerRadius);
            Gizmos.DrawLine(inVRight,outVRight);
            Gizmos.DrawLine(inVLeft,outVLeft);
        }

        // float p = angThresh;
        // float x = Mathf.Sqrt(1 - p * p);

        // Vector3 vLeftDir = new Vector3(-x,0,p);
        // Vector3 vRightDir = new Vector3(x,0,p);

        // Vector3 outVLeft = vLeftDir * outerRadius;
        // Vector3 outVRight = vRightDir * outerRadius;
        // Vector3 outVUp = new Vector3(0,x,p) * outerRadius;
        // Vector3 outVDown = new Vector3(0,-x,p) * outerRadius;

        // Vector3 inVLeft = vLeftDir * innerRadius;
        // Vector3 inVRight = vRightDir * innerRadius;
        // Vector3 inVUp = new Vector3(0,x,p) * innerRadius;
        // Vector3 inVDown = new Vector3(0,-x,p) * innerRadius;
        

        // float innerCircleRadius = outVRight.x;
        // float outerCircleRadius = inVRight.x;

        // Vector3 innerCircleCenter =  new Vector3(0,0,outVRight.z) + Vector3.zero;
        // Vector3 outerCircleCenter =  new Vector3(0,0,inVRight.z) + Vector3.zero;

        // Handles.DrawWireDisc(innerCircleCenter,Vector3.forward,innerCircleRadius);
        // Handles.DrawWireDisc(outerCircleCenter,Vector3.forward,outerCircleRadius);

        // Handles.DrawWireArc(default,Vector3.up,outVLeft,fovDeg,outerRadius);
        // Handles.DrawWireArc(default,Vector3.up,inVLeft,fovDeg,innerRadius);
        // Gizmos.DrawLine(inVRight,outVRight);
        // Gizmos.DrawLine(inVLeft,outVLeft);

        // Handles.DrawWireArc(default,Vector3.right,outVUp,fovDeg,outerRadius);
        // Handles.DrawWireArc(default,Vector3.right,inVUp,fovDeg,innerRadius);
        // Gizmos.DrawLine(inVUp,outVUp);
        // Gizmos.DrawLine(inVDown,outVDown);
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

        if(flatDistance > outerRadius || flatDistance < innerRadius)
            return false; // outside the cylinder

        return true;    
    }

    public bool SphericalContains(Vector3 position)
    {
        Vector3 vecToTargetWorld = position - transform.position;   
        Vector3 vecToTarget = transform.InverseTransformVector(vecToTargetWorld);

        float disToTarget = vecToTarget.magnitude; //Vector3.Distance(transform.position,position)
        
        if(disToTarget > outerRadius || disToTarget < innerRadius)
            return false;

        return true;
    }

    public bool SphericalSectorContains(Vector3 position)
    {
        if(!SphericalContains(position))
            return false;

        Vector3 dirToTarget = (position - transform.position).normalized;
        float projAngle = Vector3.Dot(transform.forward, dirToTarget);
        return projAngle > angThresh;


        // float angleRad = AngleBetweenNormalizedVectors(transform.forward, dirToTarget); 
        // return angleRad < fovRad/2;


        // Vector3 vecToTargetWorld = (position - transform.position);
        // Vector3 vecToTarget = transform.InverseTransformVector(vecToTargetWorld);

        // float disToTarget = vecToTarget.magnitude;
        // Vector3 dirToTarget = vecToTarget / disToTarget;

        // if(dirToTarget.z < angThresh && dirToTarget.x < angThresh)
        //     return false;

        // if(disToTarget > outerRadius)
        //     return false;   

        // return true;
    }

    static float AngleBetweenNormalizedVectors(Vector3 a, Vector3 b)
    {
        return Mathf.Clamp(Mathf.Acos(Vector3.Dot(a, b)),-1,1);
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
