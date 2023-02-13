using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour {
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTempalte;

    void Awake() {
        iconTempalte.gameObject.SetActive(false);
    }

    void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }


    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        foreach(Transform child in transform) {
            if(child != iconTempalte) {
                Destroy(child.gameObject);
            }
        }

        plateKitchenObject.KitchenObjectSOList.ForEach(kitchenObjectSO => {
            var iconTransform = Instantiate(iconTempalte, transform);

            iconTransform.gameObject.SetActive(true);

            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        });
    }
}
