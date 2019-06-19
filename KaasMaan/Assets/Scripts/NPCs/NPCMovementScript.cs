using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementScript : MonoBehaviour{
    private static int amountOfCivs;
    private GameObject parent;
    public GameObject NPC;
    public GameObject spawnCenter;

    private float speed;
    public int bulletpointLayer = 1;
    public float parallaxSpeedIncreasePerLayer = 0.01f;

    private ArrayList NPCs = new ArrayList();

    public Transform planetCenter;
    private float bulletpointsDistance = 0.475f;
    public Transform planetSprite;

    public Transform mainPlanet;



    // Start is called before the first frame update
    void Start() {
        spawnCenter = Instantiate(spawnCenter);
        spawnCenter.transform.position = new Vector3(planetCenter.position.x, planetCenter.position.y);
        spawnCenter.transform.SetParent(mainPlanet);

    }

    // Update is called once per frame
    void Update(){
        speed = Random.Range(1, 15);
        amountOfCivs = GameManager.amountOfCivilians;
        SpawnNPCs(amountOfCivs, planetSprite.localScale.x);
        spawnCenter.transform.Rotate(new Vector3(0, 0, (-speed * (1 + bulletpointLayer * parallaxSpeedIncreasePerLayer)) * Time.deltaTime));
    }

    public void SpawnNPCs(int amountOfPeople, float planetSize) {

        ArrayList newNPCs = new ArrayList();

        for (int i = NPCs.Count; amountOfPeople > i; i++) {



            float ang = i * 360;
            float xPos = planetCenter.localPosition.x + planetSize * bulletpointsDistance * Mathf.Sin(ang * Mathf.Deg2Rad);
            float yPos = planetCenter.localPosition.y + planetSize * bulletpointsDistance * Mathf.Cos(ang * Mathf.Deg2Rad);



            GameObject go = Instantiate(NPC);
            go.transform.localPosition = new Vector3(xPos + 0.5f, yPos + 0.5f, 0);
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
}
