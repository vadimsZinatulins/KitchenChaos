using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
    [SerializeField] private CuttingCounter counter;
    [SerializeField] private Image barImage;
    
    void Start() {
        counter.OnProgressChanged += CuttingCounter_OnProgressChanged;

        SetFillAmount(0);
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e) {
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
