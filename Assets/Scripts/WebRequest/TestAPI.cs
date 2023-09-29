using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using CuteEngine;
using CuteEngine.Utilities.Converter;
using System;
using Mono.Cecil.Cil;
using CuteEngine.Utilities.Networking;
using Unity.VisualScripting;

public class TestAPI : MonoBehaviour
{
    static string url = "http://localhost:3000";
    
    static string GetData = "/GetData/";
    static string AddOneData = "/AddOneData/"; //Use ID
    static string AddManyData = "/AddManyData/";
    static string UpdateData = "/UpdateData/"; //Use ID
    static string DeleteData = "/DeleteData/"; //Use ID

    string data;

    public string ID ="";

    [ContextMenuItem("GetAllDataRequest","InvokeGetAllDataRequest")]
    [ContextMenuItem("GetDataByIDRequest","InvokeGetDataByIDRequest")]
    [ContextMenuItem("PostDataRequest","PostDataRequest")]
    [ContextMenuItem("PutDataRequest","InvokePutDataRequest")]
    [ContextMenuItem("DeleteDataRequest","InvokeDeleteDataRequest")]
    [ContextMenuItem("TestGetAllDataRequest","TestGetAllDataRequest")]
    [ContextMenuItem("TestGetDataByIDRequest","TestGetDataByIDRequest")]
    public APIClass apiClass;
    public APIClass[] apiClassArray;

    async void InvokeGetAllDataRequest()
    {
        GetDataRequest();
    }

    void TestGetAllDataRequest()
    {
        apiClassArray = JsonHelper.DeserializeTextToObject<APIClass[]>(data);
    }

    void InvokeGetDataByIDRequest()
    {
        GetDataRequest(ID);
    }

    void TestGetDataByIDRequest()
    {
        apiClass = JsonHelper.DeserializeTextToObject<APIClass>(data);
    }

    void InvokePutDataRequest()
    {
        PutDataRequest(ID);
    }

    void InvokeDeleteDataRequest()
    {
        DeleteDataRequest(ID);
    }

    void GetDataRequest(string id = null)
    {
        StartCoroutine(APIHelper.MakeGetRequest(url+GetData,SetData,id));
    }

    void PostDataRequest()
    {
        List<string> test = new List<string>(){"a","a"};

        APIClass _apiClass = new APIClass("Panit","Student",5,test);

        StartCoroutine(APIHelper.MakePostRequest(url+AddOneData,_apiClass));
    }

    void PutDataRequest(string id = null)
    {
        APIClass _apiClass = new APIClass("Suebsakuntong","Car",null,null);
        StartCoroutine(APIHelper.MakePutRequest(url+UpdateData,_apiClass,id));
    }

    void DeleteDataRequest(string id = null)
    {
        StartCoroutine(APIHelper.MakeDeleteRequest(url+DeleteData,id));
    }

    void SetData(string _data)
    {
        data = _data;
    }
}

[Serializable]
public class APIClass
{
    public string name;
    public string category;
    public float? price;
    public List<string> tags;

    public APIClass(string _name,string _category,float? _price,List<string> _tag)
    {
        name = _name;
        category = _category;
        price = _price;
        tags = _tag;
    }
}
