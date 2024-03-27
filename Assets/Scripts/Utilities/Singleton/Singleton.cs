using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CuteEngine.Utilities
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            Init();
        }

        protected abstract void InitAfterAwake();

        protected Singleton()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                string message = $"There are 2 or more {typeof(T).Name} in scene. Plase remove it. Or it will auto remove when start game";
                throw new Exception(message);
            }
        }

        protected virtual void Init()
        {
            if (instance == null)
            {
                instance = this as T;
                InitAfterAwake();
            }
            else
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void Destroy()
        {
            if (Instance != null)
                Destroy(gameObject);
        }
    }
}
