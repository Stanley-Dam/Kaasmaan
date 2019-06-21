using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingBackground : MonoBehaviour {

    public GameObject camera;
    public float viewZone = 5;
    public Transform[] layers = new Transform[3];
    public float backgroundSize = 19.2f;
    public float paralaxSpeed = 0f;

    private int leftIndex;
    private int rightIndex;

    // Start is called before the first frame update
    void Start() {
        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    private void scrollLeft() {
        int lastRight = rightIndex;
        float yPos = layers[leftIndex].localPosition.y;

        layers[rightIndex].localPosition = Vector3.right * (layers[leftIndex].localPosition.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;

        if (rightIndex < 0) {
            rightIndex = layers.Length - 1;
        }

        //Always set the y position to the yPos
        layers[rightIndex].localPosition = new Vector3(layers[rightIndex].localPosition.x, yPos, layers[rightIndex].localPosition.z);
    }

    // Update is called once per frame
    void Update() {
        foreach (Transform currentLayer in layers) {
            currentLayer.localPosition += new Vector3(paralaxSpeed * Time.deltaTime, 0, 0);
        }

        if (camera.transform.localPosition.x < layers[leftIndex].transform.localPosition.x + viewZone)
            scrollLeft();
    }
}
