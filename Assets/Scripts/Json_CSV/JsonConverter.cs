using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;

public class JsonConverter
{
    const string PROJECTFOLDER = "OwnEngine";

#region SaveJSON
    public static void SaveJSONAsObject(string fileName,object saveObject,bool streaming = false)
    {
        string data = JsonConvert.SerializeObject(saveObject,Formatting.Indented);

        if(streaming)
            CreateStreamingJSON(fileName,data);
        else
            CreateUserJSON(fileName,data);  
    }   
    
    static void CreateStreamingJSON(string fileName,string data)
    {
        if(Application.isEditor)
        {
            StreamWriter writer;
            FileInfo t = new FileInfo(Application.streamingAssetsPath + "/" + fileName);
            if(!t.Exists)
                t.Directory.Create();
            else
                t.Delete();

            writer = t.CreateText();
            writer.Write(data);
            writer.Close();
        }
    }

    static void CreateUserJSON(string fileName,string data)
    {
        if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            StreamWriter writer;
            FileInfo t = new FileInfo(Application.persistentDataPath + "/" + PROJECTFOLDER + "/" + fileName);
            
            if(!t.Exists)
                t.Directory.Create();
            else
                t.Delete();

            writer = t.CreateText();
            writer.Write(data);
            writer.Close();
        }
        else if(Application.isEditor)
        {
            StreamWriter writer;
            FileInfo t = new FileInfo(Application.dataPath + "/../../Documents/" + PROJECTFOLDER + "/" + fileName);
            
            if(!t.Exists) 
                t.Directory.Create();
            else
                t.Delete();

            writer = t.CreateText();
            writer.Write(data);
            writer.Close();
        }
    }

    static void DeleteUserJSON(string fileName)
    {
        if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            FileInfo t = new FileInfo(Application.persistentDataPath + "/" + PROJECTFOLDER + "/" + fileName);
            
            if(t.Exists)
                t.Delete();
        }
        else if(Application.isEditor)
        {
            FileInfo t = new FileInfo(Application.dataPath + "/../../Documents/" + PROJECTFOLDER + "/" + fileName);
            
            if(t.Exists)
                t.Delete();
        }
    }
#endregion

#region LoadJSON
    public static T LoadJSONAsObject<T>(string fileName)
    {
        var data = LoadTextAppData(fileName);

        if(data != string.Empty)
        {
            try 
            {
                return (T)JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                    });
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return default(T);
            }
        }
        else
            return default(T);
    }

    public static T LoadJSONAsObject<T>(string fileName,Newtonsoft.Json.JsonConverter converter)
    {
        var data = LoadTextAppData(fileName);

        if(data != string.Empty)
        {
            try 
            {
                return (T)JsonConvert.DeserializeObject<T>(data,converter);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return default(T);
            }
        }
        else
            return default(T);
    }

    public static T LoadUserJSONAsObject<T>(string fileName)
    {
        var data = LoadTextAppData(fileName);

        if(data != string.Empty)
        {
            try 
            {
                return (T)JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                    });
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return default(T);
            }
        }
        else
            return default(T);
    }

    static string LoadTextUserData(string fileName)
    {
        string filePath;

        if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            filePath = Application.persistentDataPath + PROJECTFOLDER + fileName;

            if(File.Exists(filePath))
            {
                StreamReader reader = File.OpenText(filePath);

                if(reader != null)
                {
                    string data = reader.ReadToEnd();
                    reader.Close();
                    return data;
                }
            }
        }
        else if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            filePath = Application.dataPath + "/../../Documents/" + PROJECTFOLDER +"/" + fileName; //Folder to save  use data when play in editor

            if(File.Exists(filePath))
            {
                StreamReader reader = File.OpenText(filePath);

                if(reader != null)
                {
                    string data = reader.ReadToEnd();
                    reader.Close();
                    return data;
                }
            }
        }
        
        return "";
    }

    static string LoadTextAppData(string fileName)
    {
        string filePath;

        if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            filePath = $"{Application.streamingAssetsPath}/{fileName}";
            
            if(File.Exists(filePath))
            {
                StreamReader reader = File.OpenText(filePath);

                if(reader != null)
                {
                    string data = reader.ReadToEnd();
                    reader.Close();
                    return data;
                }
            }
        }
        else if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            filePath = $"{Application.streamingAssetsPath}/{fileName}";
            
            if(File.Exists(filePath))
            {
                StreamReader reader = File.OpenText(filePath);

                if(reader != null)
                {
                    string data = reader.ReadToEnd();
                    reader.Close();
                    return data;
                }
            }
        }
        
        return "";
    }
#endregion
}
