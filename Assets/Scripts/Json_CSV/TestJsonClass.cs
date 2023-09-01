using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestJsonClass : MonoBehaviour
{
    public TestClass testClass;
    void Start()
    {
        testClass = testClass.LoadData();
    }

}

[Serializable]
public class TestClass
{
    public string text = "Test Json";
    public int num = 10;
    public float num2 = 55.45f;
    public bool toggle = false;

    public void SaveData()
    {
        JsonConverter.SaveJSONAsObject("testJson.json",this,true);
    }

    public TestClass LoadData()
    {
        return JsonConverter.LoadJSONAsObject<TestClass>("testJson.json");
    }
}
