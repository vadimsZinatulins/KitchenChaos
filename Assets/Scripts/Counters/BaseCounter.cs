using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenParentObject {

    [SerializeField] private Transform topPoint;
    
    public KitchenObject KitchenObject { get; set; }
    
    public bool HasKitchenObject => KitchenObject != null;

    public void ClearKitchenObject() {
        KitchenObject = null;
    }
    
    public Transform GetKitchenObjectFollowTransform() {
        return topPoint;
    }

    public virtual void Interact(PlayerController player) {

    }

    public virtual void InteractAlterante(PlayerController player) {

    }
}
