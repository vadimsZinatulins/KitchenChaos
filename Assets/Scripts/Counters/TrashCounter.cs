using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter {

    public override void Interact(PlayerController player) {
        if(player.HasKitchenObject) {
            player.KitchenObject.DestroySelf();
        }
    }

}
