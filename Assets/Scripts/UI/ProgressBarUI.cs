using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
    [SerializeField] private GameObject objectWithProgress;
    [SerializeField] private Image barImage;

    private IHasProgress hasProgressComponent;
    
    void Start() {
        hasProgressComponent = objectWithProgress.GetComponent<IHasProgress>();
        
        hasProgressComponent.OnProgressChanged += ObjectWithProgress_OnProgressChanged;

        SetFillAmount(0);
    }

    private void ObjectWithProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        SetFillAmount(e.progressNormalized);
    }

    private void SetFillAmount(float amount) {
        barImage.fillAmount = amount;

        if(amount <= 0f || amount >= 1f) {
            Hide();
        } else {
            Show();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
