using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour {

    private int CUT = Animator.StringToHash("Cut");

    [SerializeField] private CuttingCounter counter;

    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start() {
        counter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, EventArgs e) {
        animator.SetTrigger(CUT);
    }
}
