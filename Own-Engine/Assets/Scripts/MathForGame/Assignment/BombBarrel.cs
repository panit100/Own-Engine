using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBarrel : MonoBehaviour
{
    public float radius = 1;

    public Transform Target;

    private void OnDrawGizmos() 
    {
        // float distance = (transform.position - Target.position).magnitude;

        // if(distance < length)
        //     isInSide = true;
        // else
        //     isInSide = false;   

        // if(!isInSide)
        //     Gizmos.color = Color.green;
        // else
        //     Gizmos.color = Color.red;

        // Gizmos.DrawWireSphere(transform.position, length);

        Vector3 center = transform.position;  

        if(Target == null)
            return;

        Vector3 targetPos = Target.position;

        Vector3 delta = center - targetPos;

        float sqrDistance = delta.x * delta.x + delta.y * delta.y + delta.z * delta.z; //Vector3.Dot(delta,delta)

        // float distance = Vector3.Distance(center,targetPos);

        bool isInside = sqrDistance < radius * radius;

        Gizmos.color = isInside ? Color.red : Color.green;
        Gizmos.DrawWireSphere(center, radius);

    }
}
