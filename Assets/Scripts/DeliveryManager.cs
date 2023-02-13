using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliveryManager : MonoBehaviour {
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeDelivered;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> watingRecipeSOList;
    
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4;
    private int watingRecipesMax = 4;

    public List<RecipeSO> WatingRecipeSOList => watingRecipeSOList;

    void Awake() {
        Instance = this;

        watingRecipeSOList = new List<RecipeSO>();
    }

    void Update() {
        spawnRecipeTimer -= Time.deltaTime;

        if(spawnRecipeTimer < 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(watingRecipeSOList.Count < watingRecipesMax) {
                var randomIndex = UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count);
                var watingRecipeSO = recipeListSO.recipeSOList[randomIndex];

                watingRecipeSOList.Add(watingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        for(int i = 0; i < watingRecipeSOList.Count; i++) {
            var waitingRecipeSO = watingRecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.KitchenObjectSOList.Count) {
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    if(!plateKitchenObject.KitchenObjectSOList.Contains(recipeKitchenObjectSO)) {
                        plateContentsMatchesRecipe = false;
                    }
                }

                if(plateContentsMatchesRecipe) {
                    watingRecipeSOList.Remove(waitingRecipeSO);

                    OnRecipeDelivered?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
