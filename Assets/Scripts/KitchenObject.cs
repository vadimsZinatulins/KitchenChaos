using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour {
    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenParentObject kitchenParentObject;

    public IKitchenParentObject Parent {
        get {
            return kitchenParentObject;
        }
        set {
            kitchenParentObject?.ClearKitchenObject();

            kitchenParentObject = value;
            kitchenParentObject.KitchenObject = this;

            transform.parent = value.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }
    }

    public KitchenObjectSO KitchenObjectSO => kitchenObjectSO;
}
