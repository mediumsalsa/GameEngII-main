using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class InputManager : MonoBehaviour
{
    // Script References
    [SerializeField] private PlayerLocomotionHandler playerLocomotionHandler;
    [SerializeField] private CameraManager cameraManager; // Reference to CameraManager


    [Header("Movement Inputs")]
    public float verticalInput;
    public float horizontalInput;
    public bool jumpInput;
    public Vector2 movementInput;
    public float moveAmount;

    public PlayerInputActions playerControls;
    private InputAction move;
    private InputAction look;
    private InputAction jump;
    private InputAction sprint;
    private InputAction playerFire;
    private InteractionManager interactionManager;


    [Header("Camera Inputs")]
    public float scrollInput; // Scroll input for camera zoom
    public Vector2 cameraInput; // Mouse input for the camera

    public bool isPauseKeyPressed = false;


    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpInput();
        HandleCameraInput();
        HandlePauseKeyInput();
        HandleInteractionInput();
    }

    private void OnEnable()
    {
        interactionManager = FindObjectOfType<InteractionManager>();
        playerControls = new PlayerInputActions();
        move = playerControls.Player.Move;
        move.Enable();

        look = playerControls.Player.Look;
        look.Enable();

        jump = playerControls.Player.Jump;
        jump.Enable();

        sprint = playerControls.Player.Sprint;
        sprint.Enable();

        playerFire = playerControls.Player.Fire;
        playerFire.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
        look.Disable();
        jump.Disable();
        sprint.Disable();
        playerFire.Disable();
    }

    private void HandleInteractionInput()
    {
        if (playerFire.IsPressed() && interactionManager.interactablePossible)
        {
            interactionManager.Interact();
        }
    }


    private void HandleCameraInput()
    {
            // Get mouse input for the camera
            cameraInput = look.ReadValue<Vector2>();
            //cameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            // Get scroll input for camera zoom
            scrollInput = Input.GetAxis("Mouse ScrollWheel");

            // Send inputs to CameraManager
            cameraManager.zoomInput = scrollInput;
            cameraManager.cameraInput = cameraInput;        
    }

    private void HandleMovementInput()
    {
        movementInput = move.ReadValue<Vector2>();
        //movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
    }

    private void HandlePauseKeyInput()
    {
        isPauseKeyPressed = Input.GetKeyDown(KeyCode.Escape); // Detect the escape key press
    }

    private void HandleSprintingInput()
    {
        if (sprint.IsPressed())
        {
            playerLocomotionHandler.isSprinting = true;
        }
        else
        {
            playerLocomotionHandler.isSprinting = false;
        }
    }

    private void HandleJumpInput()
    {
        jumpInput = jump.IsPressed();
        if (jumpInput)
        {
            playerLocomotionHandler.HandleJump(); // Trigger jump in locomotion handler
        }
    }



}
