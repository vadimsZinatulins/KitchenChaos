using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    private int IS_WALKING = Animator.StringToHash("IsWalking");

    [SerializeField]
    private PlayerController playerController;
    private Animator animator;
    
    void Awake() {
        animator = GetComponent<Animator>();
        animator.SetBool(IS_WALKING, playerController.IsWalking);
    }

    void Update() {
        animator.SetBool(IS_WALKING, playerController.IsWalking);
    }
}
