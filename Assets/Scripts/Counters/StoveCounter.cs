using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnStateChangedEventArgs : EventArgs {
        public bool on;
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    private float fryingTimer;

    void Update() {
        if(HasKitchenObject && HasRecipeWithInput(KitchenObject.KitchenObjectSO)) {
            fryingTimer += Time.deltaTime;

            FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(KitchenObject.KitchenObjectSO);
            if(fryingTimer > fryingRecipeSO.fryingTime) {
                fryingTimer = 0f;

                KitchenObject.DestroySelf();

                KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{ on = HasRecipeWithInput(KitchenObject.KitchenObjectSO) });
            }

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = fryingTimer / fryingRecipeSO.fryingTime
            });
        }
    }

    public override void Interact(PlayerController player) {
        if(!HasKitchenObject && player.HasKitchenObject) {
            if(HasRecipeWithInput(player.KitchenObject.KitchenObjectSO)) {
                player.KitchenObject.Parent = this;

                fryingTimer = 0f;
                
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{ on = true });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
            }
        } else if(HasKitchenObject && !player.HasKitchenObject) {
            KitchenObject.Parent = player;
            
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{ on = false });
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
        } else if(HasKitchenObject && player.HasKitchenObject) {
            if(player.KitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                if(plateKitchenObject.TryAddIngredient(KitchenObject.KitchenObjectSO)) {
            
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{ on = false });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });

                    KitchenObject.DestroySelf();
                }
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO input) {
        return GetFryingRecipeSOWithInput(input) != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input) {
        return GetFryingRecipeSOWithInput(input)?.output;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO input) {
        foreach(FryingRecipeSO recipe in fryingRecipeSOArray) {
            if(recipe.input == input) {
                return recipe;
            }
        }

        return null;
    }
}
