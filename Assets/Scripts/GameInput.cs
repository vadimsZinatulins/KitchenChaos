using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameInput : MonoBehaviour {

    public event EventHandler OnInteraction;
    public event EventHandler OnInteractionAlterante;

    private PlayerInputActions playerInputActions;

    void Awake() {
        playerInputActions = new PlayerInputActions();

        playerInputActions.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlterante_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteraction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlterante_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractionAlterante?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        return inputVector.normalized;
    }
}
