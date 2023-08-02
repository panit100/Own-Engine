using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class TestingRandom : MonoBehaviour
{
    public AnimationCurve animationCurve;
    public int randomNumber;

    // [SerializeField]
    // MinMaxCurve ab = new MinMaxCurve(1f,new AnimationCurve(),new AnimationCurve());
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            RandomByAnimationCurve();
        }
    }

    void RandomByAnimationCurve()
    {
        var random = Random.Range(0f,100f);
        print(random);
        randomNumber = Mathf.CeilToInt(animationCurve.Evaluate(random));
    }
}
