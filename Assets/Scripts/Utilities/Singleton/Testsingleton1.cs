using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testsingleton1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Testsingleton.Instance.Test();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
