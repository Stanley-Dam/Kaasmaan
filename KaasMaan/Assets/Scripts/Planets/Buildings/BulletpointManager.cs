using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletpointManager : MonoBehaviour {

    public Transform planetCenter;
    public Transform planetSprite;
    public GameObject bulletpointPrefab;
    public Camera cam;

    public int buildingsPerSize = 10;

    private ArrayList bulletPoints = new ArrayList();

    // Start is called before the first frame update
    void Start() {
        generateBulletpoints(planetSprite.localScale.x);
    }

    private void Update() {
        if(Input.GetKey(KeyCode.A)) {
            planetSprite.localScale += new Vector3(1, 1);
            generateBulletpoints(planetSprite.localScale.x);
            cam.orthographicSize++;
        }
    }

    public void generateBulletpoints(float planetSize) {

        updateBulletPoints(planetSize);

        ArrayList newBulletPoints = new ArrayList();

        //Bulletpoint generate algorithm
        for (int i = 0; i < (planetSize * buildingsPerSize); i++) {

            float ang = i * (360 / (planetSize * buildingsPerSize)) + bulletPoints.Count;
            float xPos = planetCenter.position.x + planetSize * 1.2f * Mathf.Sin(ang * Mathf.Deg2Rad);
            float yPos = planetCenter.position.y + planetSize * 1.2f * Mathf.Cos(ang * Mathf.Deg2Rad);

            //Check if the position is available
            if (getBulletpointAtPosition(new Vector3(xPos, yPos, 0)) != null) continue;

            Quaternion rotation = Quaternion.AngleAxis(ang, Vector3.forward);

            GameObject currentObject = Instantiate(bulletpointPrefab, new Vector3(xPos, yPos, 0), rotation);
            currentObject.transform.SetParent(this.transform, false);

            //Add bullet point to the arraylist so we can get our information later on
            newBulletPoints.Add(currentObject);
        }

        foreach (GameObject currentObject in newBulletPoints) {
            bulletPoints.Add(currentObject);
        }
    }

    private void updateBulletPoints(float planetSize) {

        if (bulletPoints.Count > 0) {
            //Move the existing bulletpoints
            int index = 0;
            foreach (GameObject currentObject in bulletPoints) {

                float ang = index * (360 / (planetSize * buildingsPerSize)) + bulletPoints.Count;
                float xPos = planetCenter.position.x + planetSize * 1.2f * Mathf.Sin(ang * Mathf.Deg2Rad);
                float yPos = planetCenter.position.y + planetSize * 1.2f * Mathf.Cos(ang * Mathf.Deg2Rad);

                Quaternion rotation = Quaternion.AngleAxis(ang, Vector3.forward);

                currentObject.transform.position = new Vector3(xPos, yPos, 0);
                currentObject.transform.rotation = rotation;

                index++;
            }
        }

    }

    public GameObject getBulletpointAtPosition(Vector3 location) {
        GameObject bulletPoint = null;

        foreach (GameObject currentObject in bulletPoints) {
            if (currentObject.transform.position.Equals(location))
                bulletPoint = currentObject;
        }

        return bulletPoint;
    }

}
