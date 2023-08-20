using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorTranformation : MonoBehaviour
{
    // public Vector3 localPos;

    // private void OnDrawGizmos() 
    // {
    //     Vector2 worldPos = LocalToWorld(localPos);
    //     Gizmos.DrawSphere(localPos,0.1f);
    //     Gizmos.DrawSphere(worldPos,0.1f);
    // }

    

    public Vector3 localPos;
    public Vector3 worldPos;

    private void OnDrawGizmos() 
    {
        localPos = WorldToLocal(worldPos);
        Gizmos.DrawSphere(worldPos,0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(localPos,0.1f);
    }

    Vector3 WorldToLocal(Vector3 worldPos)
    {
        // return transform.InverseTransformPoint(worldPos);
        
        // return transform.worldToLocalMatrix.MultiplyPoint3x4(worldPos);

        Vector3 rel = worldPos - transform.position;
        float x = Vector3.Dot(rel, transform.right); //X axis
        float y = Vector3.Dot(rel, transform.up); //Y axis
        float z = Vector3.Dot(rel, transform.forward);

        return new(x,y,z);

    }

    Vector3 LocalToWorld(Vector3 localPos)
    {
        Matrix4x4 mtx = Matrix4x4.TRS(new Vector3(1,5,4),Quaternion.identity,Vector3.one);

        // return transform.TransformPoint(localPos);

        // return transform.localToWorldMatrix.MultiplyPoint3x4(localPos);

        // return transform.localToWorldMatrix * new Vector4(localPos.x,localPos.y,localPos.z,1);

        Vector3 position = transform.position;

        //Can not use position + loaclPos
        position += localPos.x * transform.right;
        position += localPos.y * transform.up;
        position += localPos.z * transform.forward;

        return position;
    }
}
