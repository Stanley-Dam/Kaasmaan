using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementScript : MonoBehaviour {
    private static int amountOfCivs;
    private GameObject parent;
    public GameObject NPC;
    public GameObject spawnCenter;

    private float speed = GameManager.planetLevel * 10;
    public int bulletpointLayer = 1;
    public float parallaxSpeedIncreasePerLayer = 0.01f;

    private ArrayList NPCs = new ArrayList();

    public Transform planetCenter;
    private float NPCDistance = 0.7f;
    public Transform planetSprite;

    public Transform mainPlanet;



    // Start is called before the first frame update
    void Start() {
        spawnCenter = Instantiate(spawnCenter);
        spawnCenter.transform.position = new Vector3(planetCenter.position.x, planetCenter.position.y);
        spawnCenter.transform.SetParent(mainPlanet);

    }

    // Update is called once per frame
    void Update() {
        amountOfCivs = GameManager.amountOfCivilians;
        SpawnNPCs(amountOfCivs, planetSprite.localScale.x);
        spawnCenter.transform.Rotate(new Vector3(0, 0, (-speed * (1 + bulletpointLayer * -parallaxSpeedIncreasePerLayer)) * Time.deltaTime));
    }

    public void SpawnNPCs(int amountOfPeople, float planetSize) {

        ArrayList newNPCs = new ArrayList();

        for (int i = NPCs.Count; i < amountOfPeople; i++) {

            int randomNumbers = Random.Range(1, 8);

            float ang = (i * 360) / (amountOfPeople * randomNumbers);
            float xPos = planetCenter.localPosition.x + planetSize * NPCDistance * Mathf.Sin(ang * Mathf.Deg2Rad);
            float yPos = planetCenter.localPosition.y + planetSize * NPCDistance * Mathf.Cos(ang * Mathf.Deg2Rad);



            GameObject go = Instantiate(NPC);
            go.transform.localPosition = new Vector3(xPos, yPos, 0);
            go.transform.SetParent(spawnCenter.transform);


            Vector3 diff = planetCenter.transform.localPosition - go.transform.localPosition;

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            diff.Normalize();
            go.transform.localRotation = Quaternion.Euler(0f, 0f, rot_z + 90);

            newNPCs.Add(go);
        }

        foreach (GameObject go in newNPCs) {
            NPCs.Add(go);
        }
    }

    public void UpdateNPCs(int amountOfPeople, float planetSize) {
        List<GameObject> NPCsList = new List<GameObject>();
        foreach (GameObject go in NPCs) {
            NPCsList.Add(go);
        }

        int randomNumbers = Random.Range(1, 8);

        NPCsList.RemoveAll(item => item == null);
        NPCs = new ArrayList();

        foreach (GameObject go in NPCsList) {
            NPCs.Add(go);
        }

        if (NPCs.Count > 0) {
            int index = 0;
            foreach (GameObject go in NPCs) {

                float ang = (index * 360) / (amountOfPeople * randomNumbers);
                float xPos = planetCenter.localPosition.x + planetSize * (NPCDistance + 0.01f) * Mathf.Sin(ang * Mathf.Deg2Rad);
                float yPos = planetCenter.localPosition.y + planetSize * (NPCDistance + 0.01f) * Mathf.Cos(ang * Mathf.Deg2Rad);

                go.transform.localPosition = new Vector3(xPos, yPos, 0);


                Vector3 diff = planetCenter.transform.localPosition - go.transform.localPosition;

                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                go.transform.localRotation = Quaternion.Euler(0f, 0f, rot_z + 90);

                index++;
            }
        }
    }
}
