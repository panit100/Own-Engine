using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int score;

    void Update()
    {
        print(CalculateGrade(score));
    }

    string CalculateGrade(int score)
    {
        if(score >= 80)
            return "A";
        else if(score >= 70)
            return "B";
        else if(score >= 60)
            return "C";
        else if(score >= 50)
            return "D";
        else
            return "F";
        
    }
}
