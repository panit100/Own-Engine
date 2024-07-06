using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TrigTest : MonoBehaviour
{
    [Range(0,360)]
    public float angDeg = 0;

    Vector2 AngToDir(float angRad) => new Vector2(Mathf.Cos(angRad),Mathf.Sin(angRad));

    void OnDrawGizmos() 
    {
        Handles.DrawWireDisc(Vector3.zero,Vector3.forward,1);

        // float angRad = angDeg * Mathf.Deg2Rad;
        // Vector2 v = AngToDir(angRad);

        // float angTurns = (float)EditorApplication.timeSinceStartup;
        // Vector2 v = AngToDir(angTurns * Mathf.PI * 2);

        float angRad = angDeg * Mathf.Deg2Rad;
        Vector2 v = AngToDir(angRad);

        Gizmos.DrawRay(default,v);    
    }
}
