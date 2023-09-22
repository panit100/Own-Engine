using System;
using System.Collections;
using System.Collections.Generic;
using CuteEngine.Utilities.Converter;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class TestClass
{
    public string text = "Test Json";
    public int num = 10;
    public float num2 = 55.45f;
    public bool toggle = false;

    public TestClass()
    {
        
    }

    public void SaveData(string fileName)
    {
        JsonConverter.SaveJSONAsObject(fileName,this,true);
    }

    public TestClass LoadData(string fileName)
    {
        return JsonConverter.LoadJSONAsObject<TestClass>(fileName);
    }
}
