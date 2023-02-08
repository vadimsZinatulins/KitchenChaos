using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour {

    private int OPEN_CLOSE = Animator.StringToHash("OpenClose");

    [SerializeField] private ContainerCounter containerCounter;

    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start() {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, EventArgs e) {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
