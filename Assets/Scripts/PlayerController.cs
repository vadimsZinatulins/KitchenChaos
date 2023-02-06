using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float movementSpeed = 5f;

    [SerializeField]
    private float rotationSpeed = 5f;

    public bool IsWalking { get; private set; }

    void Update() {
        Vector2 inputVector = new Vector2(0, 0);

        if(Input.GetKey(KeyCode.W)) {
            inputVector.y += 1;
        }
        
        if(Input.GetKey(KeyCode.A)) {
            inputVector.x -= 1;
        }
        
        if(Input.GetKey(KeyCode.S)) {
            inputVector.y -= 1;
        }
        
        if(Input.GetKey(KeyCode.D)) {
            inputVector.x += 1;
        }

        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y).normalized;

        IsWalking = moveDirection != Vector3.zero;
        
        transform.position += moveDirection * movementSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
    }
}
