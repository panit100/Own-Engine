using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CuteEngine.Player.Controller
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float speed = 10f;
        [SerializeField] float jumpForce = 10f;
        [SerializeField] float jumpTime = 10f;

        Vector3 direction;

        float checkGroundRange = .6f;
        bool isOnGround = true;
        
        Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();

            AddAction();
        }

        void AddAction()
        {
            PlayerManager.Instance.PlayerController.onMove += OnMove;
            PlayerManager.Instance.PlayerController.onJump += OnJump;
        }   

        void RemoveAction()
        {
            PlayerManager.Instance.PlayerController.onMove -= OnMove;
            PlayerManager.Instance.PlayerController.onJump -= OnJump;
        }

        void Update()
        {
            Move();
            // CheckGround();
        }

        void Move()
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        void Jump()
        {
            if(!isOnGround)
                return;
                
            rb.AddForce(Vector3.up * jumpForce,ForceMode.Acceleration);

            isOnGround = false;
        }

        // void CheckGround()
        // {
        //     if(Physics.Raycast(transform.position,Vector3.down,checkGroundRange,LayerMask.GetMask("Ground")))
        //     {
        //         isOnGround = true;
        //     }
        // }

        void OnMove(Vector2 value)
        {
            direction = new Vector3(value.x,0,value.y);
        }

        void OnJump()
        {
            Jump();
        }

        void OnDestroy() 
        {
            RemoveAction();
        }

        void OnDrawGizmos() 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position,Vector3.down * checkGroundRange);
        }

        void OnCollisionEnter(Collision other) 
        {
            if(other.gameObject.CompareTag("Ground"))
            {
                isOnGround = true;
            }
        }

        void OnCollisionExit(Collision other) 
        {
            if(other.gameObject.CompareTag("Ground"))
            {
                isOnGround = false;
            }
        }
    }
}
