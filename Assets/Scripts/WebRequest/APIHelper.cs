using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace CuteEngine.Utilities.Networking
{
    public class APIHelper
    {   
        /// <summary>
        /// Create a unity web request by using URL and request type.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static UnityWebRequest CreateRequest(string url,RequestType type = RequestType.GET,HeaderSetting[] headerSettings = null,object data = null) //TODO: implement upload later when own api finish
        {
            var request = new  UnityWebRequest(url,type.ToString());

            if(data != null)
            {
                var bodyRaw = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data,Formatting.Indented,new JsonSerializerSettings {
                    NullValueHandling = NullValueHandling.Ignore
                }));
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            }

            request.downloadHandler = new DownloadHandlerBuffer();

            AttachHeader(request,"Content-Type","application/json");

            if(headerSettings != null)
            {
                foreach(var n in headerSettings)
                {
                    AttachHeader(request,n.key,n.value);
                }
            }

            return request;
        }

        /// <summary>
        /// Communicating to the specified URL.Get Data from URL.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callBack"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerator MakeGetRequest(string url,Action<string> getData = null,string id = "",HeaderSetting[] headerSettings = null)
        {
            string _url = url;
            if(id != string.Empty)
                _url += id;

            var request = CreateRequest(_url,RequestType.GET,headerSettings);
            yield return request.SendWebRequest();
            getData(request.downloadHandler.text);

            request.Dispose();
            Debug.Log($"Get Data : {url}");
        }

        public static IEnumerator MakeGetRequestByID(string url,Action<string> getData,HeaderSetting[] headerSettings = null)
        {
            var request = CreateRequest(url,RequestType.GET,headerSettings);
            yield return request.SendWebRequest();
            getData(request.downloadHandler.text);

            request.Dispose();
            Debug.Log($"Get Data : {url}");
        }

        public static IEnumerator MakePostRequest(string url,object data,Action<string> getData = null)
        {
            var request = CreateRequest(url,RequestType.POST,null,data);
            yield return request.SendWebRequest();

            if(getData != null)
                getData(request.downloadHandler.text);

            request.Dispose();
            Debug.Log($"Post Data : {url}");
        }

        public static IEnumerator MakePutRequest(string url,object data,string id = "",Action<string> getData = null)
        {
            var request = CreateRequest(url+id,RequestType.PUT,null,data);
            yield return request.SendWebRequest();

            if(getData != null)
                getData(request.downloadHandler.text);

            request.Dispose();
            Debug.Log($"Post Data : {url}");
        }

        public static IEnumerator MakeDeleteRequest(string url,string id = "")
        {
            var request = CreateRequest(url+id,RequestType.DELETE);
            yield return request.SendWebRequest();
            Debug.Log($"Delete Data : {url}");
        }

        public static void AttachHeader(UnityWebRequest request, string key, string value)
        {
            request.SetRequestHeader(key,value);
        }
    }

    public class HeaderSetting
    {
        public string key;
        public string value;

        public HeaderSetting(string _key,string _value)
        {
            key = _key;
            value = _value;
        }
    }

    public enum RequestType
    {
        GET = 0,
        POST = 1,
        PUT = 2,
        DELETE = 3
    }
}
