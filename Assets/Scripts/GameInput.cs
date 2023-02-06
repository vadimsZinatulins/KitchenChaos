using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameInput : MonoBehaviour {

    public event EventHandler OnInteraction;

    private PlayerInputActions playerInputActions;

    void Awake() {
        playerInputActions = new PlayerInputActions();

        playerInputActions.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteraction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        return inputVector.normalized;
    }
}
