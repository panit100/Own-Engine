using System.Collections;
using System.Collections.Generic;
using CuteEngine.Utilities;
using UnityEngine;

public class Testsingleton : PersistentSingleton<Testsingleton>
{
    protected override void InitAfterAwake()
    {
    }

    public void Test()
    {
        print("test");
    }
}
