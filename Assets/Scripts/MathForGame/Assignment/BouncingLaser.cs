using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingLaser : MonoBehaviour
{
    public int bouncingCount;

    // Ray ray;
    // LineRenderer lineRenderer;

    // Vector2 reflecDirection;
    // Vector2 hitpoint;
    void Start()
    {
        // lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // ray = new Ray(transform.position, transform.right);
        // Physics.Raycast(ray,out RaycastHit hit,float.PositiveInfinity);

        // lineRenderer.positionCount = bouncingCount;
        // lineRenderer.SetPosition(0,transform.position);

        // lineRenderer.SetPosition(1,hit.point);

        // float dot = Vector2.Dot(hit.normal,hit.point.normalized);
        // reflecDirection = hit.point.normalized - 2 * dot * hit.normal;

        // hitpoint = hit.point;
        // Ray _ray = new Ray(hit.point, reflecDirection);
        // Physics.Raycast(_ray,out RaycastHit _hit,float.PositiveInfinity);

        // lineRenderer.SetPosition(2,_hit.point);
    }

    private void OnDrawGizmos() {
        GetComponent<LineRenderer>().positionCount = bouncingCount+2;
        RaycastHit hit;

        Vector2 origin = transform.position;
        Vector2 dir = transform.right;
        Ray ray = new Ray(origin, dir);

        Gizmos.DrawLine(origin, origin + dir);
        
        GetComponent<LineRenderer>().SetPosition(0, origin);

        for(int i = 0; i < bouncingCount+1; i++)
        {
            if(Physics.Raycast(ray,out hit))
            {
                GetComponent<LineRenderer>().SetPosition(i+1, hit.point);
                Gizmos.DrawSphere(hit.point,0.1f);
                Vector2 reflecDirection = ReflectDir(ray.direction,hit.normal);
                Gizmos.DrawLine(hit.point, (Vector2)hit.point + reflecDirection);
            }

            ray = new Ray(hit.point, ReflectDir(ray.direction,hit.normal));
        }


        // ray = new Ray(hit.point, reflecDirection);

        // if(Physics.Raycast(ray,out hit))
        // {
        //     Gizmos.DrawSphere(hit.point,0.1f);
        // }
        
        // dot = Vector2.Dot(reflecDirection,hit.normal);
        // reflecDirection = reflecDirection - 2 * dot * hit.normal;

        // Gizmos.DrawLine(hit.point, hit.point + reflecDirection);
    }


    Vector2 ReflectDir(Vector2 inDir,Vector2 normalSurface)
    {
        float proj = Vector2.Dot(inDir,normalSurface);
        return inDir - 2 * proj * normalSurface;
    }
}
