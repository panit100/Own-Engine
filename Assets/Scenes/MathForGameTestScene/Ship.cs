using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Transform crewLocation;
    public Transform crewTran;

    void OnDrawGizmos() 
    {
        if(crewTran == null)
            return;

        crewTran.position = transform.TransformPoint(crewLocation.localPosition);
    }
}
