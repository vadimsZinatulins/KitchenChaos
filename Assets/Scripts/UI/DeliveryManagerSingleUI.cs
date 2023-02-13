using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryManagerSingleUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO) {
        recipeNameText.text = recipeSO.recipeName;

        foreach(Transform child in iconContainer) {
            if(child != iconTemplate) {
                Destroy(child.gameObject);
            }
        }

        recipeSO.kitchenObjectSOList.ForEach(kitchenObjectSO => {
            var iconTransform = Instantiate(iconTemplate, iconContainer);

            iconTransform.gameObject.SetActive(true);

            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        });
    }
}
