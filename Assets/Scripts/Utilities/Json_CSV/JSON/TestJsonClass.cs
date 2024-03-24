using System;
using System.Collections;
using System.Collections.Generic;
using CuteEngine.Utilities.Converter;
using UnityEngine;

[Serializable]
public class TestClass
{

    public string text { get { return Text; } set { Text = value; } }
    public int num { get { return Num; } set { Num = value; } }
    public float num2 { get { return Num2; } set { Num2 = value; } }
    public bool toggle { get { return Toggle; } set { Toggle = value; } }

    public string Text;
    public int Num;
    public float Num2;
    public bool Toggle;

    public void SaveData(string fileName)
    {
        JsonHelper.SaveJSONAsObject(fileName, this, true);
    }

    public TestClass LoadData(string fileName)
    {
        return JsonHelper.LoadJSONAsObject<TestClass>(fileName);
    }
}
