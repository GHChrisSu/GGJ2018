using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInteraction))]
public class PlayerController : MonoBehaviour {
    PlayerMovement playerMovement;
    PlayerInteraction playerInteraction;
    Player player;
    string playerRef = "";

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerInteraction = GetComponent<PlayerInteraction>();
        player = GetComponent<Player>();
        playerRef = player.playerName;
    }

    private void Update()
    {
        ControllerFixedUpdate();
    }

    private void ControllerFixedUpdate()
    {
        float horizontal = CrossPlatformInputManager.GetAxis(playerRef + "Horizontal");
        float vertical = CrossPlatformInputManager.GetAxis(playerRef + "Vertical");

        //Debug.Log("Horziontal:" + horizontal + " Vertical:" + vertical);

        
        playerMovement.Move(horizontal, vertical);

        if(CrossPlatformInputManager.GetButtonDown(playerRef + "Fire1"))
        {
            playerInteraction.Interact();
        }

    }
}
