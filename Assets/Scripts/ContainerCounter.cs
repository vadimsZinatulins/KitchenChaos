using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter {
    
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectPrefab;

    override public void Interact(PlayerController player) {
        if(!player.HasKitchenObject) {
            KitchenObject.SpawnKitchenObject(kitchenObjectPrefab, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
