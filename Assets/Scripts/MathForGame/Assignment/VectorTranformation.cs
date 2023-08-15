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

    Vector3 LocalToWorld(Vector3 localPos)
    {
        Vector2 position = transform.position;

        //Can not use position + loaclPos
        position += localPos.x * (Vector2)transform.right;
        position += localPos.y * (Vector2)transform.up;

        return position;
    }

    public Vector3 localPos;
    public Vector3 worldPos;

    private void OnDrawGizmos() 
    {
        localPos = WorldToLocal(worldPos);
        Gizmos.DrawSphere(worldPos,0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(localPos,0.1f);
    }

    Vector2 WorldToLocal(Vector3 worldPos)
    {
        Vector2 rel = worldPos - transform.position;
        float x = Vector2.Dot(rel, transform.right); //X axis
        float y = Vector2.Dot(rel, transform.up); //Y axis

        return new Vector2(x,y);

    }
}
