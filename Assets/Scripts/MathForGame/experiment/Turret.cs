using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    public TurretTrigger turretTrigger;
    public Transform gunTransform;

    public float smoothingFactor = 1f;

    void Update() 
    {
        Quaternion targetRotation;

        if(turretTrigger.Contains(target.position))
        {
            //world space
            Vector3 vecToTarget = target.position - gunTransform.position;
            targetRotation = Quaternion.LookRotation(vecToTarget,transform.up);
        }
        else
        {
            targetRotation = Quaternion.LookRotation(transform.forward,transform.up);
        }
        
            gunTransform.rotation = Quaternion.Slerp(gunTransform.rotation,targetRotation,smoothingFactor * Time.deltaTime);
    }
}
