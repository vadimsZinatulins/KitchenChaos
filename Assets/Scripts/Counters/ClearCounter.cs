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
            if(player.HasKitchenObject) {
                if(player.KitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    if(plateKitchenObject.TryAddIngredient(KitchenObject.KitchenObjectSO)) {
                        KitchenObject.DestroySelf();
                    }
                } else {
                    if(KitchenObject.TryGetPlate(out plateKitchenObject)) {
                        if(plateKitchenObject.TryAddIngredient(player.KitchenObject.KitchenObjectSO)) {
                            player.KitchenObject.DestroySelf();
                        }
                    }
                }
            } else {
                KitchenObject.Parent = player;
            }
        }
    }
}
