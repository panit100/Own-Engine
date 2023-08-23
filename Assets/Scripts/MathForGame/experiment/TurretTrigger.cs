using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TurretTrigger : MonoBehaviour
{   
    public Transform target;

    public float range = 1;
    public float height = 1;     
    // [Range(0f, 1f)]
    // public float angThresh = 0.5f; //not an actual

    [Range(0, 180)]
    public float fovDeg = 45; //not an actual
    float angThresh => Mathf.Cos(fovDeg * Mathf.Deg2Rad / 2);

    void OnDrawGizmos() 
    {
        if(target == null)
            return;

        //make gizmos relative to this transform
        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;

        Gizmos.color = Handles.color = Contains(target.position) ? Color.red:Color.white;

        Vector3 top = new Vector3(0,height,0);

        Handles.DrawWireDisc(default,Vector3.up,range);
        Handles.DrawWireDisc(top,Vector3.up,range);

        float p = angThresh;
        float x = Mathf.Sqrt(1 - p * p);

        Vector3 vLeft = new Vector3(-x,0,p) * range;
        Vector3 vRight = new Vector3(x,0,p) * range;

        Gizmos.DrawRay(default,vRight);
        Gizmos.DrawRay(default,vLeft);
        Gizmos.DrawRay(top,vRight);
        Gizmos.DrawRay(top,vLeft);

        Gizmos.DrawLine(default,top);
        Gizmos.DrawLine(vRight,top+vRight);
        Gizmos.DrawLine(vLeft,top+vLeft);
    }

    public bool Contains(Vector3 position)
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

        if(flatDistance > range || flatDistance < 0)
            return false; // outside the cylinder

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
