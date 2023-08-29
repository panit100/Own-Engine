using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class TurretPlacer : MonoBehaviour
{
    float pitchDeg;
    float yawDeg;

    float turretYawOffsetDeg;

    public float mouseSensitivity = 1;
    public float turretyawSensitivity = 1;
    public Transform turret;

    void Awake() 
    {
        Vector3 startEuler = transform.eulerAngles;
        pitchDeg = startEuler.x;
        yawDeg = startEuler.y;    

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        UpdateTurrentYawInput();
        UpdateMoveLook();
        PlaceTurret();
    }

    void UpdateMoveLook()
    {
        if(Cursor.lockState == CursorLockMode.None)
            return;

        float xDelta = Input.GetAxis("Mouse X");
        float yDelta = Input.GetAxis("Mouse Y");

        pitchDeg += -yDelta * mouseSensitivity;
        pitchDeg = Mathf.Clamp(pitchDeg,-90,90);
        yawDeg += xDelta * mouseSensitivity;

        transform.rotation = Quaternion.Euler(pitchDeg, yawDeg,0);
    }
    
    void UpdateTurrentYawInput()
    {
        float scrollDelta = Input.mouseScrollDelta.y;
        turretYawOffsetDeg += scrollDelta * turretyawSensitivity;

        if(turretYawOffsetDeg < -90) turretYawOffsetDeg = -90;
        if(turretYawOffsetDeg > 90) turretYawOffsetDeg = 90;
    }

    void PlaceTurret()
    {
        Ray ray = new Ray(transform.position,transform.forward);
        
        if(Physics.Raycast(ray, out RaycastHit hit))
        {   
            turret.position = hit.point;
            
            Vector3 yAxis = hit.normal;
            Vector3 zAxis = Vector3.Cross(transform.right,yAxis);

            Debug.DrawLine(ray.origin, hit.point,new Color(1, 1, 1,0.4f));
            Quaternion offsetRotation = Quaternion.Euler(0,turretYawOffsetDeg,0);
            turret.rotation = Quaternion.LookRotation(zAxis,yAxis) * offsetRotation;
        }
    }

    // void OnDrawGizmos() 
    // {   
    //     if(turret == null)  
    //         return;

    //     Ray ray = new Ray(transform.position,transform.forward);

    //     if(Physics.Raycast(ray, out RaycastHit hit))
    //     {   
    //         turret.transform.position = hit.point;
            
    //         Vector3 yAxis = hit.normal;
    //         // Grahm-Schmidt orthonormalization
    //         Vector3 xAxis = Vector3.Cross(yAxis,ray.direction).normalized;
    //         Vector3 zAxis = Vector3.Cross(transform.right,yAxis);


    //         Gizmos.color = Color.red;
    //         Gizmos.DrawRay(hit.point,xAxis);

    //         Gizmos.color = Color.green;
    //         Gizmos.DrawRay(hit.point,yAxis);

    //         Gizmos.color = Color.blue;
    //         Gizmos.DrawRay(hit.point,zAxis);

    //         Gizmos.color = Color.white;
    //         Gizmos.DrawLine(ray.origin,hit.point);

    //         turret.rotation = Quaternion.LookRotation(zAxis,yAxis);
    //     }
    // }
}
