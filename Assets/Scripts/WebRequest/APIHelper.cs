using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using CuteEngine.Utilities.Converter;
using Unity.VisualScripting;
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
        public static UnityWebRequest CreateRequest(string url,RequestType type = RequestType.GET) //TODO: implement upload later when own api finish
        {
            var request = new  UnityWebRequest(url,type.ToString());

            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type","application/json"); //TODO: Add another header when have it

            return request;
        }

        /// <summary>
        /// Communicating to the specified URL.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="callBack"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerator MakeRequest(string url,Action<string> callBack,RequestType type = RequestType.GET)
        {
            var request = CreateRequest(url,type);
            yield return request.SendWebRequest();
            callBack(request.downloadHandler.text);
        }
    }

    public enum RequestType
    {
        GET = 0,
        POST = 1,
        PUT = 2,
    }
}
