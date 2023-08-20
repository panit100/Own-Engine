using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    public TurretTrigger turretTrigger;
    public Transform gunTransform;

    void OnDrawGizmos() 
    {

        if(turretTrigger.Contains(target.position))
        {
            //world space
            Vector3 vecToTarget = target.position - gunTransform.position;
            gunTransform.rotation = Quaternion.LookRotation(vecToTarget,transform.up);
        }
        else
        {
            gunTransform.rotation = Quaternion.Euler(Vector3.forward);    
        }
    }
}
