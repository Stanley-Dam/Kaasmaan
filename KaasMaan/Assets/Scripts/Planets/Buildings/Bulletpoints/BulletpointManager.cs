using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletpointManager : MonoBehaviour {

    public BuildingManager buildingManager;

    public Transform planetCenter;
    public Transform planetSprite;
    public GameObject bulletpointPrefab;
    public Camera cam;

    public int buildingsPerSize = 10;

    private ArrayList bulletPoints = new ArrayList();

    private float bulletpointsDistance = 0.475f;

    // Start is called before the first frame update
    void Start() {
        planetSprite.localScale = new Vector3(2.5f, 2.5f);
        generateBulletpoints(planetSprite.localScale.x);
    }

    private void Update() {
        //Testing the planet upscaling
        if(Input.GetKeyDown(KeyCode.S)) {
            planetSprite.localScale += new Vector3(1, 1);
            generateBulletpoints(planetSprite.localScale.x);
            GameManager.planetLevel++;
        }
    }

    public void generateBulletpoints(float planetSize) {

        updateBulletPoints(planetSize);
        ArrayList newBulletPoints = new ArrayList();

        //Bulletpoint generate algorithm
        for (int i = bulletPoints.Count; i < (buildingsPerSize + bulletPoints.Count); i++) {
            float ang = i * 360 / (buildingsPerSize + bulletPoints.Count);
            float xPos = planetCenter.localPosition.x + planetSize * bulletpointsDistance * Mathf.Sin(ang * Mathf.Deg2Rad);
            float yPos = planetCenter.localPosition.y + planetSize * bulletpointsDistance * Mathf.Cos(ang * Mathf.Deg2Rad);

            //Check if the position is available
            if (getBulletpointAtPosition(new Vector3(xPos, yPos, 0)) != null) continue;

            GameObject currentObject = Instantiate(bulletpointPrefab);
            currentObject.transform.position = new Vector3(xPos, yPos, 0);

            currentObject.transform.SetParent(planetCenter.transform, false);

            //Rotate the bulletpoint
            Vector3 diff = planetCenter.localPosition - currentObject.transform.localPosition;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            currentObject.transform.localRotation = Quaternion.Euler(0f, 0f, rot_z + 90);

            //Add bullet point to the arraylist so we can get our information later on
            newBulletPoints.Add(currentObject);
        }

        foreach (GameObject currentObject in newBulletPoints) {
            bulletPoints.Add(currentObject);
            buildingManager.CreateBuilding(0, currentObject, 0);
        }
    }

    private void updateBulletPoints(float planetSize) {

        if (bulletPoints.Count > 0) {
            //Move the existing bulletpoints
            int index = 0;
            foreach (GameObject currentObject in bulletPoints) {

                float ang = index * 360 / (buildingsPerSize + bulletPoints.Count);
                float xPos = planetCenter.localPosition.x + planetSize * bulletpointsDistance * Mathf.Sin(ang * Mathf.Deg2Rad);
                float yPos = planetCenter.localPosition.y + planetSize * bulletpointsDistance * Mathf.Cos(ang * Mathf.Deg2Rad);

                currentObject.transform.localPosition = new Vector3(xPos, yPos, 0);

                //Rotate the bulletpoint
                Vector3 diff = planetCenter.localPosition - currentObject.transform.localPosition;
                diff.Normalize();
                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                currentObject.transform.localRotation = Quaternion.Euler(0f, 0f, rot_z + 90);

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
