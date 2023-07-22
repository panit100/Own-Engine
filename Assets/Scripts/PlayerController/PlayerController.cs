using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CuteEngine.InputSystem;

namespace CuteEngine.Player3D
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float speed = 10f;

        void Start()
        {
            
            
        }

        void Update()
        {

        }
    }
}
