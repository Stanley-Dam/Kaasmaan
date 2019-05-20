using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    public GameObject shopMenu;
    public GameObject equipPrefab;
    public List<GameObject> createdObjects = new List<GameObject>();
    private float minX, maxX, minY, maxY;
    private Vector3 position;
    private GameObject go;

    void Start() {
        if (shopMenu.active == true) {
            shopMenu.active = false;
        }

        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = bottomCorner.x;
        maxX = topCorner.x;
        minY = bottomCorner.y;
        maxY = topCorner.y;

    }

    private void Update() {
    if(createdObjects.Count == 1) {
            DeselectedBuilding();
        }
    }

    public void OpenShop() {
        if (shopMenu.active == true) {
            shopMenu.active = false;
        }
        else {
            shopMenu.active = true;
        }
    }

    public void SelectedBuilding() {
        if (equipPrefab != null) {
            if (createdObjects.Count != 1) {
                //Vector3 position = new Vector3(Random.Range(minX, maxX - 0.5f), Random.Range(minY + 0.5f, maxY - 0.5f), 0);
                position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);

                go = (GameObject)Instantiate(equipPrefab, position, Quaternion.identity);
                createdObjects.Add(go);
                }
        }

    }
    public void DeselectedBuilding() {
        if (Input.GetMouseButtonDown(1)) {
            Destroy(go);
            createdObjects.Clear();
        }
    }
}
