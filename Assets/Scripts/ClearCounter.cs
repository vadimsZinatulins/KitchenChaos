using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {
    
    [SerializeField] private KitchenObjectSO kitchenObjectPrefab;

    override public void Interact(PlayerController player) {
        if(!HasKitchenObject) {
            if(player.HasKitchenObject) {
               player.KitchenObject.Parent = this; 
            }
        } else {
            if(!player.HasKitchenObject) {
                KitchenObject.Parent = player;
            }
        }
    }
}
