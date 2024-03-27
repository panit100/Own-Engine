using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testsingleton1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", 3f);
    }

    // Update is called once per frame
    void Destroy()
    {
        Testsingleton.Instance.Destroy();
    }
}
