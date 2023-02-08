using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress {
    public event EventHandler OnCut;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    override public void Interact(PlayerController player) {
        if(!HasKitchenObject) {
            if(player.HasKitchenObject) {
                cuttingProgress = -1;
                UpdateProgress(1);
                
               player.KitchenObject.Parent = this; 
            }
        } else {
            if(!player.HasKitchenObject) {
                KitchenObject.Parent = player;
            }
        }
    }

    public override void InteractAlterante(PlayerController player) {
        if(HasKitchenObject && GetCuttingRecipeSO(KitchenObject.KitchenObjectSO, out CuttingRecipeSO recipe)) {
            if(Cut(recipe.maxCuttingProgress) >= recipe.maxCuttingProgress) {
                KitchenObject.DestroySelf();

                KitchenObject.SpawnKitchenObject(recipe.output, this);
            }
        }
    }

    private bool GetCuttingRecipeSO(KitchenObjectSO input, out CuttingRecipeSO recipe) {
        recipe = null;

        foreach(CuttingRecipeSO r in cuttingRecipeSOArray) {
            if(r.input == input) {
                recipe = r;
                return true;
            }
        }

        return false;
    }

    private int Cut(float max) {
        OnCut?.Invoke(this, EventArgs.Empty);

        return UpdateProgress(max);
    }

    private int UpdateProgress(float max) {
        ++cuttingProgress;

        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
            progressNormalized = cuttingProgress / max
        });

        return cuttingProgress;
    }
}
