using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public Transform subPlanet;
    public float offset;

    public float rotateSpeed;
    float angle = 0;

    public float waitingTime;

    void Update()
    {
        if(waitingTime > 0)
        {
            waitingTime -= Time.deltaTime;
            return;
        }

        if (subPlanet == null)
            return;
        
        RotatePlanet();

        SubPlanetOrbit();
    }

    void RotatePlanet()
    {
        angle += rotateSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0,angle,0);
    }

    void SubPlanetOrbit()
    {
        Vector3 subPlanetPos = (transform.TransformDirection(transform.forward) * offset) + transform.position ;
        subPlanet.position = subPlanetPos;
    }
}
