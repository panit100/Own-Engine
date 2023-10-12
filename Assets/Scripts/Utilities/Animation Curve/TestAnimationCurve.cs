using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimationCurve : MonoBehaviour
{
    public AnimationCurve animationCurve;
    public float multiplyScale;
    float time;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        float newYPosition = animationCurve.Evaluate(time);
        transform.position = new Vector3(transform.position.x,newYPosition,transform.position.z) * multiplyScale;
    }
}
