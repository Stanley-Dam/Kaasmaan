using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Housing : MonoBehaviour {

    private int civiliansPerLevel = 5;

    public MainBulletpoint mainBulletpoint;

    // Start is called before the first frame update
    void Start() {
        //Add the new civilians to the world
        GameManager.amountOfCivilians += civiliansPerLevel * mainBulletpoint.getBuilding().getLevel();
    }
}
