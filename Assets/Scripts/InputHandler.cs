using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInputs playerInputs;

    private void OnEnable()
    {
        if (playerInputs == null)
        {
            playerInputs = new PlayerInputs();
        }

        playerInputs.Player.Enable();
        playerInputs.Player.Jump.performed += HandlePlayerJump;
    }

    private void OnDisable()
    {
        playerInputs.Player.Jump.performed -= HandlePlayerJump;
        playerInputs.Player.Disable();
    }

    private void HandlePlayerJump(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            PlayerMovement.Instance.Jump();
        }
    }

    public Vector2 GetLookAroundVector()
    {
        Vector2 lookInput = playerInputs.Player.LookAround.ReadValue<Vector2>() * Time.smoothDeltaTime;

        return lookInput;
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputs.Player.Move.ReadValue<Vector2>();

        return inputVector;

    }



}
