using System.Collections;
using System.Collections.Generic;
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
    static string url = "https://api.chucknorris.io/jokes/random";
    string data;

    [ContextMenuItem("GetDataRequest","GetDataRequest")]
    public APIClass apiClass;

    void GetDataRequest()
    {
        StartCoroutine(APIHelper.MakeRequest(url,GetData));
    }

    void GetData(string _data)
    {
        data = _data;
        TestRequest();
    }

    void TestRequest()
    {
        apiClass = JsonConverter.DeserializeTextToObject<APIClass>(data);

        print($"created_at + {apiClass.created_at}");
        print($"updated_at + {apiClass.updated_at}");
    }
}

[Serializable]
public class APIClass
{
    public string[] categories;
    public DateTime created_at;
    public string icon_url;
    public string id;
    public DateTime updated_at;
    public string url;
    public string value;
}
