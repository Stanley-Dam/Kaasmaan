using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building {

    /*
     * Holds all specific building's data
     */

    private int buildingID;
    private int buildingType;
    private GameObject bulletpoint;
    private int level;

    public Building(int buildingID, int buildingType, GameObject bulletpoint, int level) {

        this.buildingID = buildingID;
        this.buildingType = buildingType;
        this.bulletpoint = bulletpoint;
        this.level = level;

    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public int getBuildingID() {
        return this.buildingID;
    }

    public GameObject getBulletpoint() {
        return bulletpoint;
    }

    public int getBuildingType() {
        return buildingType;
    }

    public int getLevel() {
        return level;
    }

}