using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Housing : MonoBehaviour {

    private int civiliansPerLevel = 5;
    private int civs = 0;

    public MainBulletpoint mainBulletpoint;

    // Start is called before the first frame update
    void Start() {
        //Add the new civilians to the world
        civs += civiliansPerLevel * mainBulletpoint.getBuilding().getLevel();
        GameManager.amountOfCivilians += civs;
    }

    public int GetCivs() {
        return civs;
    }

    public void LevelUp() {
        civs += (civs + civiliansPerLevel);
        GameManager.amountOfCivilians += civiliansPerLevel;
    }
}
