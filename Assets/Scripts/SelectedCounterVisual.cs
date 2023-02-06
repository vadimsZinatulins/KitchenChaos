using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {

    [SerializeField]
    private ClearCounter counter;

    [SerializeField]
    private Transform visualGameObject;

    void Start() {
        PlayerController.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, PlayerController.OnSelectedCounterChangedEventArgs e) {
        if(e.counter == counter) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        visualGameObject.gameObject.SetActive(true);
    }

    private void Hide() {
        visualGameObject.gameObject.SetActive(false);
    }
}
