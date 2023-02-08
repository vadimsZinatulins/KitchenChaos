using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour {
    
    [SerializeField] private PlatesCounter counter;
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> platesVisualGameObjectList;

    void Awake() {
        platesVisualGameObjectList = new List<GameObject>();
    }

    void Start() {
        counter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        counter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e) {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, topPoint);

        float plateOffset = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0f, plateOffset * platesVisualGameObjectList.Count, 0f);

        platesVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e) {
        GameObject plateGameObject = platesVisualGameObjectList[platesVisualGameObjectList.Count - 1];
        platesVisualGameObjectList.Remove(plateGameObject);
        
        Destroy(plateGameObject);
    }
}
