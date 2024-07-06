using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathThing : MonoBehaviour
{
    public Transform A;
    public Transform B; 

    public float scProj;

    public Vector2 vecProj;

    void OnDrawGizmos() 
    {
        Vector2 a = A.position;
        Vector2 b = B.position; 

        Gizmos.color = Color.red;
        Gizmos.DrawLine(default,a);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(default,b);

        // Normalize A 
        // mannual
        // float aLength = Mathf.Sqrt(a.x * a.x + a.y * a.y);
        // float aLength = a.magnitude;
        // Vector2 aNorm = a/aLength;

        Vector2 aNorm = a.normalized;
        Vector2 bNorm = b.normalized;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(aNorm,0.1f);



        //Scalar projection
        scProj = Vector2.Dot(aNorm,b);

        //Vector projection
        vecProj = aNorm * scProj;
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(vecProj,0.1f);

        //Test
        // a is normalize vector

        // scProj = Vector2.Dot(aNorm,b);
        // vecProj = aNorm * scProj;
        Vector2 _b = b - vecProj;
        Vector2 reflectVec = (-2 * _b ) + b;

        Gizmos.DrawSphere(reflectVec,0.1f);
    }
}
