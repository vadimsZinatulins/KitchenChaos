using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter {
    
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectPrefab;

    public bool HasKitchenObject => KitchenObject != null;

    override public void Interact(PlayerController player) {
        if(!player.HasKitchenObject) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectPrefab.prefab);

            kitchenObjectTransform.GetComponent<KitchenObject>().Parent = player;

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
