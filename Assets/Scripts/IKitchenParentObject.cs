using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenParentObject
{
    public KitchenObject KitchenObject { get; set; }

    public bool HasKitchenObject => KitchenObject != null;

    public void ClearKitchenObject();
    public Transform GetKitchenObjectFollowTransform();

}
