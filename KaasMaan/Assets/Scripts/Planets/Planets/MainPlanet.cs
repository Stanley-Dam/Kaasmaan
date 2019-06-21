using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlanet : MonoBehaviour {

    public float speed = 5;
    public Transform[] layers = new Transform[2];
    public Transform blackHole;
    public Transform center;
    public int bulletpointLayer = 1;
    public float parallaxSpeedIncreasePerLayer = 0.01f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        float index = 1;

        foreach(Transform currentLayer in layers) {
            currentLayer.transform.Rotate(new Vector3(0, 0, (-speed * index) * Time.deltaTime));
            index += parallaxSpeedIncreasePerLayer;
        }
        blackHole.Rotate(new Vector3(0, 0, (-speed * index) * Time.deltaTime));
        blackHole.localScale = new Vector3(GameManager.blackHoleLevel/GameManager.planetLevel, GameManager.blackHoleLevel/ GameManager.planetLevel);

        center.Rotate(new Vector3(0, 0, (-speed * (1 + bulletpointLayer * parallaxSpeedIncreasePerLayer)) * Time.deltaTime));

    }
}
