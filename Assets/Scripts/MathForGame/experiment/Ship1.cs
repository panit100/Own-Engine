using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship1 : MonoBehaviour
{
    public Transform target;
    public Transform crewTran;

    void OnDrawGizmos() 
    {
        if(crewTran == null)
            return;

        crewTran.localPosition = transform.InverseTransformVector(target.position);
    }
}
