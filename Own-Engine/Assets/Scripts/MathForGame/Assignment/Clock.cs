using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public bool moveSmoothly;
    public bool use24Hour;

    [Range(0f,1f)]
    public float hourLength = 1f;
    [Range(0f,1f)]
    public float minLength = 1f;
    [Range(0f,1f)]
    public float secLength = 1f;

    [Range(0,0.2f)]
    public float tickSizeSecMin = 0.05f;

    [Range(0,0.5f)]
    public float tickSizeHour = 0.1f;

    Vector2 vHour;
    Vector2 vMin;
    Vector2 vSec;

    const float TAU = 6.28318530718f;

    int HoursOnClock => use24Hour ? 24 : 12;
    void OnDrawGizmos() 
    {
        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;
        DateTime time = DateTime.Now;
        
        ShowNormalHour(time);

        // if(!fullHour)
        // {
            // CreateNormalHourTickMark();
            // CreateNormalMinTickMark();
        // }
        // else
        // {
            // ShowFullHour(time);
            // CreateFullHourTickMark();
            // CreateFullMinTickMark();
        // }

        for(int i = 0;i < 60;i++)
        {
            Vector2 dir = SecondsOrMinutesToDirection(i);
            DrawTick(dir,tickSizeSecMin,1);
        }

        for(int i = 0;i < HoursOnClock;i++)
        {
            Vector2 dir = HourToDirection(i);
            DrawTick(dir,tickSizeHour,3);
        }
        
        DrawClockHand(vHour,hourLength,5,Color.white);
        DrawClockHand(vMin,minLength,2,Color.white);
        DrawClockHand(vSec,secLength,1,Color.red);

        Handles.DrawWireDisc(default,Vector3.forward,1f);
    }

    Vector2 AngToDir(float angleRad) => new Vector2(Mathf.Cos(angleRad),Mathf.Sin(angleRad));

    Vector2 ValueToDirection(float value, float valueMax)
    {
        // 0-1 value representing "percent"/fraction along the 0-valueMax range
        float t = value / valueMax;         
        return FractionToClockDir(t);
    }

    Vector2 HourToDirection(float hour)
    {
        return ValueToDirection(hour,HoursOnClock);
    }

    Vector2 SecondsOrMinutesToDirection(float secOrMin)
    {
        return ValueToDirection(secOrMin,60);
    }

    Vector2 FractionToClockDir(float t)
    {
        float angleRad = (0.25f - t) * TAU; //minus 0.25f to offset start point to 90 degress
        return AngToDir(angleRad);
    }

    void ShowNormalHour(DateTime time)
    {
        float seconds = time.Second;
        if(moveSmoothly)
            seconds += time.Millisecond / 1000f;

        vSec = SecondsOrMinutesToDirection(seconds);
        vMin = SecondsOrMinutesToDirection(time.Minute);
        vHour = HourToDirection (time.Hour);

        // float hourDeg = time.Hour * 15;
        // float hourReg = (hourDeg + 90) * Mathf.Deg2Rad;

        // float minDeg = time.Minute * 6;
        // float minReg = (minDeg + 90) * Mathf.Deg2Rad;

        // float secDeg = time.Second * 6;
        // float secReg = (secDeg + 90) * Mathf.Deg2Rad;

        // vHour = new Vector2(-Mathf.Cos(hourReg),Mathf.Sin(hourReg));
        // vMin = new Vector2(-Mathf.Cos(minReg),Mathf.Sin(minReg));
        // vSec = new Vector2(-Mathf.Cos(secReg),Mathf.Sin(secReg));
    }

    void DrawTick(Vector2 dir,float length,float thickness)
    {
        Handles.DrawLine(dir,dir * (1f-length),thickness);
    }

    void DrawClockHand(Vector2 dir, float length,float thickness,Color color)
    {
        using(new Handles.DrawingScope(color))
            Handles.DrawLine(default,dir * length,thickness);
    }

    // void CreateNormalHourTickMark()
    // {
    //     Gizmos.color = Color.white;

    //     for(int i = 1;  i <= 12; i++)
    //     {
    //         float tickReg = (i * 30) * Mathf.Deg2Rad;
    //         Vector2 tickPosition = new Vector2(Mathf.Cos(tickReg), Mathf.Sin(tickReg));

    //         Gizmos.DrawRay(tickPosition,tickPosition * .2f);
    //     }
    // }

    // void CreateNormalMinTickMark()
    // {
    //     Gizmos.color = Color.white;

    //     for(int i = 1;  i <= 60; i++)
    //     {
    //         float tickReg = (i * 6) * Mathf.Deg2Rad;
    //         Vector2 tickPosition = new Vector2(Mathf.Cos(tickReg), Mathf.Sin(tickReg));

    //         Gizmos.DrawRay(tickPosition,tickPosition * .1f);
    //     }
    // }

    // void ShowFullHour(DateTime time)
    // {
        // float hourDeg = time.Hour * 30;
        // float hourReg = (hourDeg + 90) * Mathf.Deg2Rad;

        // float minDeg = time.Minute * 3;
        // float minReg = (minDeg + 90) * Mathf.Deg2Rad;

        // float secDeg = time.Second * 3;
        // float secReg = (secDeg + 90) * Mathf.Deg2Rad;

        // vHour = new Vector2(-Mathf.Cos(hourReg),Mathf.Sin(hourReg));
        // vMin = new Vector2(-Mathf.Cos(minReg),Mathf.Sin(minReg));
        // vSec = new Vector2(-Mathf.Cos(secReg),Mathf.Sin(secReg));

        // vHour = HourToDirection (time.Hour);
        // vMin = SecondsOrMinutesToDirection(time.Minute);
        // vSec = SecondsOrMinutesToDirection(time.Second);
    // }

    // void CreateFullHourTickMark()
    // {
    //     Gizmos.color = Color.white;

    //     for(int i = 1;  i <= 24; i++)
    //     {
    //         float tickReg = (i * 15) * Mathf.Deg2Rad;
    //         Vector2 tickPosition = new Vector2(Mathf.Cos(tickReg), Mathf.Sin(tickReg));

    //         Gizmos.DrawRay(tickPosition,tickPosition * .2f);
    //     }
    // }

    // void CreateFullMinTickMark()
    // {
    //     Gizmos.color = Color.white;

    //     for(int i = 1;  i <= 120; i++)
    //     {
    //         float tickReg = (i * 3) * Mathf.Deg2Rad;
    //         Vector2 tickPosition = new Vector2(Mathf.Cos(tickReg), Mathf.Sin(tickReg));

    //         Gizmos.DrawRay(tickPosition,tickPosition * .1f);
    //     }
    // }
}
