using System;
using System.Collections;
using System.Collections.Generic;
using CuteEngine.Utilities.Converter;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class TestClass
{

    public string text { get; set; }
    public int num { get; set; }
    public float num2 { get; set; }
    public bool toggle { get; set; }


    public void SaveData(string fileName)
    {
        JsonHelper.SaveJSONAsObject(fileName, this, true);
    }

    public TestClass LoadData(string fileName)
    {
        return JsonHelper.LoadJSONAsObject<TestClass>(fileName);
    }
}
