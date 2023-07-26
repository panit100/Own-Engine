using System.Collections;
using System.Collections.Generic;
using CuteEngine.Utilities;
using UnityEngine;
using CuteEngine.Player.Controller;

namespace CuteEngine.Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        PlayerController playerController;
        PlayerMovement playerMovement;

        public PlayerController PlayerController {get {return playerController;}}
        public PlayerMovement PlayerMovement {get {return playerMovement;}}

        protected override void InitAfterAwake()
        {
            playerController = GetComponent<PlayerController>();
            playerMovement = GetComponent<PlayerMovement>();
        }
    }
}

