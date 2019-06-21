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

    public void setBuildingType(BuildingTypes buildingType) {
        this.buildingType = buildingType.getTypeId();
    }

    public int getBuildingID() {
        return this.buildingID;
    }

    public GameObject getBulletpoint() {
        return bulletpoint;
    }

    public int getBuildingTypeID() {
        return buildingType;
    }

    public BuildingTypes getBuildingType() {
        return BuildingTypes.getBuildingFromTypeID(this.buildingType);
    }

    public int getLevel() {
        return level;
    }

    public void setLevel(int level) {
        this.level = level;
    }

    public void LevelUp() {
        this.level *= 2;
    }

}