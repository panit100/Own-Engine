using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CuteEngine.InputSystem;

namespace CuteEngine.Player.Controller
{
    public class PlayerController : MonoBehaviour
    {
        public UnityAction<Vector2> onMove;
        public UnityAction onJump;
        

        void Start()
        {
            if(InputSystemManager.Instance != null)
                AddInputActionWithInputSystemManager();
        }

        void AddInputActionWithInputSystemManager()
        {
            InputSystemManager.Instance.onMove += OnMove;
            InputSystemManager.Instance.onJump += OnJump;
        }

        void RemoveInputActionWithInputSystemManager()
        {
            InputSystemManager.Instance.onMove -= OnMove;
            InputSystemManager.Instance.onJump -= OnJump;
        }

        void OnMove(Vector2 value)
        {
            onMove?.Invoke(value);
        }

        void OnJump()
        {
            onJump?.Invoke();
        }

        void OnDestroy() 
        {
            if(InputSystemManager.Instance != null)
                RemoveInputActionWithInputSystemManager();
        }
    }
}
