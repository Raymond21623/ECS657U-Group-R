using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerMovement playerMovement;

    public Vector2 movementInput;

    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float verticalInput;
    public float horizontalInput;

    public bool jumpInput;

    public bool sprintInput;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new PlayerInput();

            playerInput.Player.Move.performed += i => movementInput = i.ReadValue<Vector2>();
            playerInput.Player.Look.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerInput.Player.Jump.performed += i => jumpInput = true;
            playerInput.Player.Jump.canceled += i => jumpInput = false;

            playerInput.Player.Sprint.performed += i => sprintInput = true;
            playerInput.Player.Sprint.canceled += i => sprintInput = false;

        }

        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();

    }
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;
    }

   /* private void HandleJumpingInput()
    {
        if (jumpInput)
        {
            playerMovement.isJumping = true;
        }
        else
        {
            playerMovement.isJumping = false;
        }
    }*/

    private void HandleSprintingInput()
    {
        if (sprintInput == true)
        {
            playerMovement.isSprinting = true;
        }
        else
        {
            playerMovement.isSprinting = false;
        }
    }


}
