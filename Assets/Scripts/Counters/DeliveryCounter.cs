using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter {
    public override void Interact(PlayerController player) {
        if(player.HasKitchenObject) {
            if(player.KitchenObject.TryGetPlate(out PlateKitchenObject plate)) {
                plate.DestroySelf();
            }
        }
    }
}
