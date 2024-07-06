using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CuteEngine.Player.Controller
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        const float GAVITY = 9.8f;

        [SerializeField] AnimationCurve speed;
        [SerializeField] float jumpHight = 10f;
        [SerializeField] AnimationCurve jumpSpeed;

        Vector3 direction;
        float maxJumpHight;

        float checkGroundRange = .6f;
        bool isOnGround = true;
        bool isFall = false;
        bool isJump = false;

        float moveTime = 0;
        float jumpTime = 0;
        
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
            CalculateJump();
            CalculateGavity();

            CheckGround();
        }

        void Move()
        {
            transform.position += direction * speed.Evaluate(moveTime) * Time.deltaTime;

            moveTime += Time.deltaTime;
            
            if(direction == Vector3.zero)
                moveTime = 0;
        }

        void Jump()
        {
            if(!isOnGround)
                return;
            
            maxJumpHight = transform.position.y + jumpHight;

            isOnGround = false;
            isFall = false;
            isJump = true;
        }

        void CalculateJump()
        {
            if(!isJump)
            {
                jumpTime = 0;
                return;
            }

            Vector3 nextPos = transform.position + new Vector3(0,1 * jumpSpeed.Evaluate(jumpTime) * Time.deltaTime,0);

            if(transform.position.y >= maxJumpHight)
            {
                isJump = false;
                isFall = true;
                return;
            }
            
            transform.position = nextPos;

            jumpTime += Time.deltaTime;
        }

        void CalculateGavity()
        {
            if(!isFall)
                return;

            Vector3 nextPos = transform.position - new Vector3(0,1 * GAVITY * Time.deltaTime,0);

            transform.position = nextPos;
        }

        void CheckGround()
        {
            if(!isFall)
                return;

            if(Physics.Raycast(transform.position,Vector3.down,checkGroundRange,LayerMask.GetMask("Ground")))
            {
                isOnGround = true;
                isFall = false;
            }
        }

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

        // void OnCollisionEnter(Collision other) 
        // {
        //     if(other.gameObject.CompareTag("Ground"))
        //     {
        //         isOnGround = true;
        //     }
        // }

        // void OnCollisionExit(Collision other) 
        // {
        //     if(other.gameObject.CompareTag("Ground"))
        //     {
        //         isOnGround = false;
        //     }
        // }
    }
}
