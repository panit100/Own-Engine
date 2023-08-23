using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public Transform subPlanet;
    public float offset;

    public float rotateSpeed;
    float radian = 0;
    float degrees = 0;
    float angle = 0;

    public float waitingTime;

    //TODO: Rotate Planet

    // void Update()
    // {
    //     if(waitingTime > 0)
    //     {
    //         waitingTime -= Time.deltaTime;
    //         return;
    //     }

    //     if (subPlanet == null)
    //         return;
        
    //     RotatePlanet();

    //     SubPlanetOrbit();
    // }

    // void RotatePlanet()
    // {
    //     radian += rotateSpeed * Time.deltaTime;

    //     degrees = radian * Mathf.Rad2Deg;

    //     transform.rotation = Quaternion.Euler(0,degrees,0);
    // }

    // void SubPlanetOrbit()
    // {
    //     Vector3 subPlanetPos = (transform.TransformDirection(transform.forward) * offset) + transform.position ;
    //     subPlanet.position = subPlanetPos;
    // }

    //TODO: Change Planet Position

    void Update()
    {
        if(waitingTime > 0)
        {
            waitingTime -= Time.deltaTime;
            return;
        }

        if (subPlanet == null)
            return;

        angle += rotateSpeed * Time.deltaTime;
    
        SubPlanetPosition();
    }

    void SubPlanetPosition()
    {
        Vector3 direction = new Vector3(Mathf.Cos(angle),0,Mathf.Sin(angle));
        subPlanet.position = transform.position + (direction * offset);
    }
}
