using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    PlayerMovement movementController;

    public void OnMove(InputAction.CallbackContext context)
    {
        movementController.Direction = context.ReadValue<Vector2>();
    }
}
