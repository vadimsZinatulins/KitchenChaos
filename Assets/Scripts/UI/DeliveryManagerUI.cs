using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour {

    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    void Awake() {
        recipeTemplate.gameObject.SetActive(false);
    }

    void Start() {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeDelivered += DeliveryManager_OnRecipeDelivered;

        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeDelivered(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in container) {
            if(child != recipeTemplate) {
                Destroy(child.gameObject);
            }
        }

        DeliveryManager.Instance.WatingRecipeSOList.ForEach(recipe => {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);

            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipe);
        });
    }
}
