using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter counter;
    }

    public static PlayerController Instance { get; private set; }

    [SerializeField]
    private GameInput gameInput;

    [SerializeField]
    private LayerMask countersLayerMask;

    [SerializeField]
    private float movementSpeed = 5f;

    [SerializeField]
    private float rotationSpeed = 5f;

    private Vector3 lastInteractionDirection;
    private ClearCounter selectedCounter;

    public bool IsWalking { get; private set; }

    private void Start() {
        gameInput.OnInteraction += GameInput_OnInteractionAction;
    }

    void Awake() {
        Instance ??= this;
    }

    void Update() {
        HandleMovemente();

        HandleInteractions();
    }

    private void GameInput_OnInteractionAction(object sender, EventArgs e) {
        selectedCounter?.Interact();
    }

    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if(moveDirection != Vector3.zero) {
            lastInteractionDirection = moveDirection;
        }

        float interactionDistance = 2f;
        if(Physics.Raycast(transform.position, lastInteractionDirection, out RaycastHit raycastHit, interactionDistance, countersLayerMask)) {
            if(raycastHit.transform.TryGetComponent<ClearCounter>(out ClearCounter clearCounter)) {
                if(selectedCounter != clearCounter) {
                    SetSelectedCounter(clearCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovemente() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        float playerHeight = 2f;
        float playerRadius = 0.7f;
        float moveDistance = movementSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if(!canMove) {
            Vector3 moveDirX = new Vector3(moveDirection.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if(canMove) {
                moveDirection = moveDirX;
            } else {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if(canMove) {
                    moveDirection = moveDirZ;
                }
            }
        }

        if(canMove) {
            transform.position += moveDirection * moveDistance;
        }

        IsWalking = moveDirection != Vector3.zero;
        
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
    }

    private void SetSelectedCounter(ClearCounter selectedCounter) {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            counter = selectedCounter
        });
    }
}
