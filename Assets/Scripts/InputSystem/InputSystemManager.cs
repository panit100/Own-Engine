using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CuteEngine.Utilities;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace CuteEngine.InputSystem
{
    public class InputSystemManager : Singleton<InputSystemManager>
    {
        const string PLAYER_ACTIONMAP = "Player";
        const string UI_ACTIONMAP = "UI";

        [SerializeField] InputActionAsset playerInputAction;

#region UnityAction

        public UnityAction<Vector2> onMove;
        public UnityAction onJump;

#endregion

        InputActionMap playerControlMap;
        InputActionMap uiControlMap;

        bool globalInputEnable = false;
        bool playerControlEnable = true;   
        bool uiControlMapEnable = true;

        protected override void InitAfterAwake()
        {
            playerControlMap = playerInputAction.FindActionMap(PLAYER_ACTIONMAP);
            uiControlMap = playerInputAction.FindActionMap(UI_ACTIONMAP);
        }

        void Start() 
        {
            ToggleGlobalInput(true);
        }

#region ToggleInput

        void ToggleGlobalInput(bool toggle)
        {
            globalInputEnable = toggle;
            UpdateInputState();
        }

        void TogglePlayerControl(bool toggle)
        {
            playerControlEnable = toggle;
            UpdateInputState();
        }

        void ToggleUIControl(bool toggle)
        {
            uiControlMapEnable = toggle;
            UpdateInputState();
        }

        void UpdateInputState()
        {
            if(globalInputEnable && playerControlEnable) playerControlMap.Enable();
            else playerControlMap.Disable();

            if(globalInputEnable && uiControlMapEnable) uiControlMap.Enable();
            else uiControlMap.Disable();
        }

#endregion

#region ControlFunction
        void OnMove(InputValue value)
        {
            onMove?.Invoke(value.Get<Vector2>());
        }

        void OnJump(InputValue value)
        {
            onJump?.Invoke();
        }
#endregion
    }
}
