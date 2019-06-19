using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawning : MonoBehaviour {

    public MainBulletpoint mainBulletpoint;
    public Factory factory;
    private Building building;

    // Start is called before the first frame update
    void Start() {
        //Instaties the smoke for the factory level 1
        switch (building.getBuildingTypeID()) {
            case 0:
                break;
            case 1:
                GameObject Smoke = Instantiate(mainBulletpoint.smoke, factory.transform.position, Quaternion.identity);
                Smoke.transform.SetParent(factory.transform);
                Smoke.transform.localPosition = new Vector3(0, 1);
                Smoke.transform.rotation = transform.rotation;
                break;
            case 2:

                break;

        }

        // Update is called once per frame
        void Update() {

        }
    }
}
