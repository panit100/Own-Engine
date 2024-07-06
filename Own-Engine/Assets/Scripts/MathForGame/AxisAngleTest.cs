using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisAngleTest : MonoBehaviour
{
    public Vector3 rotation;

    private void OnDrawGizmos() {
        Vector3 forward = transform.forward;

        Quaternion rot = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    
        Vector3 rotateVector = rot * forward;

        Gizmos.color = Color.red; 
        Gizmos.DrawLine(transform.position,forward);
        Gizmos.color = Color.green; 
        Gizmos.DrawLine(transform.position,rotateVector);

    }
}
