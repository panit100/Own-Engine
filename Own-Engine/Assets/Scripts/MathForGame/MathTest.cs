using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathTest : MonoBehaviour
{
    public Transform obj1;
    public Transform obj2;

    public Vector3 testVec;

    void OnDrawGizmos() 
    {
        Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;

        // localToWorldMatrix.MultiplyPoint3x4() //Ignore last row and faster

        //Tranforming form one space to another
        //OR Tranforming between local and world
        // transform.TransformPoint() //M * (v.x,v.y,v.z,1)
        // transform.InverseTransformPoint() //M^-1 * (v.x,v.y,v.z,1)
        // transform.TransformVector() //M * (v.x,v.y,v.z,0)
        // transform.InverseTransformVector() //M^-1 * (v.x,v.y,v.z,0)
        
        Gizmos.DrawSphere(obj1.TransformPoint(obj2.position), 0.1f);

        testVec = obj1.TransformPoint(obj2.position);
    }
}
