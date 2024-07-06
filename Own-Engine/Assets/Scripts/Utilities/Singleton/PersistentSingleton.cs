using System.Collections;
using System.Collections.Generic;
using CuteEngine.Utilities;
using UnityEngine;

namespace CuteEngine.Utilities
{
    public class PersistentSingleton<T> : Singleton<T> where T : Singleton<T>
    {
        protected override void InitAfterAwake()
        {

        }

        protected override void Init()
        {
            base.Init();
            if (Application.isPlaying)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
