using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CuteEngine.Utilities.Converter;
using System;
using CuteEngine.Utilities.Networking;

public class TestAPI : MonoBehaviour
{
    public TestClass testClass;

    private void Start()
    {
        testClass = JsonHelper.LoadJSONAsObject<TestClass>("test");
    }

    static string url = "http://localhost:4001";

    static string GetData = "/GetData/";
    static string AddOneData = "/AddOneData/"; //Use ID
    static string AddManyData = "/AddManyData/";
    static string UpdateData = "/UpdateData/"; //Use ID
    static string DeleteData = "/DeleteData/"; //Use ID

    string data;

    public string ID = "";
    public string email = "";
    public string password = "";


    [ContextMenuItem("LoginRequest", "LoginRequest")]
    [ContextMenuItem("TestLoginRequestData", "TestLoginRequestData")]
    public UserClass userClass;

    [ContextMenuItem("GetAllDataRequest", "InvokeGetAllDataRequest")]
    [ContextMenuItem("GetDataByIDRequest", "InvokeGetDataByIDRequest")]
    [ContextMenuItem("PostDataRequest", "PostDataRequest")]
    [ContextMenuItem("PutDataRequest", "InvokePutDataRequest")]
    [ContextMenuItem("DeleteDataRequest", "InvokeDeleteDataRequest")]
    [ContextMenuItem("TestGetAllDataRequest", "TestGetAllDataRequest")]
    [ContextMenuItem("TestGetDataByIDRequest", "TestGetDataByIDRequest")]
    public APIClass apiClass;
    public APIClass[] apiClassArray;

    void InvokeGetAllDataRequest()
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
        // PutDataRequest(ID);
    }

    void InvokeDeleteDataRequest()
    {
        DeleteDataRequest(ID);
    }

    void GetDataRequest(string id = null)
    {
        StartCoroutine(APIHelper.MakeGetRequest(url + GetData, SetData, id, new HeaderSetting[] { new HeaderSetting("x-access-token", userClass.token) }));
    }

    // void PostDataRequest()
    // {
    //     List<string> test = new List<string>(){"a","a"};

    //     APIClass _apiClass = new APIClass("Panit","Student",5,test);

    //     StartCoroutine(APIHelper.MakePostRequest(url+AddOneData,_apiClass));
    // }

    // void PutDataRequest(string id = null)
    // {
    //     APIClass _apiClass = new APIClass("Suebsakuntong","Car",null,null);
    //     StartCoroutine(APIHelper.MakePutRequest(url+UpdateData,_apiClass,id));
    // }

    void DeleteDataRequest(string id = null)
    {
        StartCoroutine(APIHelper.MakeDeleteRequest(url + DeleteData, id));
    }

    void SetData(string _data)
    {
        data = _data;
        print(data);
    }

    void LoginRequest()
    {
        LoginData loginData = new LoginData(email, password);

        StartCoroutine(APIHelper.MakePostRequest(url + "/login", loginData, SetData));
    }

    void TestLoginRequestData()
    {
        userClass = JsonHelper.DeserializeTextToObject<UserClass>(data);
    }
}

[Serializable]
public class APIClass
{
    public int product_id;
    public string product_name;
    public int product_price;
}


[Serializable]
public class UserClass
{
    public string first_name;
    public string last_name;
    public string email;
    public string password;
    public string token;
}

public class LoginData
{
    public string email;
    public string password;

    public LoginData(string _email, string _password)
    {
        email = _email;
        password = _password;
    }
}
