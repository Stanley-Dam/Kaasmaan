using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBulletpoint : MonoBehaviour {

    public SpriteRenderer spriteRederer;

    private Building building;
    private bool hasBuildingSprite = false;

    public GameObject smoke;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButton(0) && this.gameObject.GetComponent<BulletpointHover>().isInRange && ShopManager.buildingTypeOnMouse != null && !hasBuildingSprite) {
            this.building.setBuildingType(ShopManager.buildingTypeOnMouse);
            this.building.setLevel(1);

            UpdateBuilding();
            ShopManager.hasPlaced();
        }
    }

    public void UpdateBuilding() {

        //Setting the sprite of the building
        string spritePath = building.getBuildingType().getSpritePath();
        string spriteName = building.getBuildingType().getSpriteName();
        spriteName += BuildingTypes.LevelToStringTranslator(Mathf.CeilToInt((building.getLevel() / building.getBuildingType().getSpriteUpgradeDiv()) + 1));

        this.spriteRederer.sprite = Resources.Load<Sprite>(spritePath + spriteName);

        //Adding the building script to the building object
        switch(building.getBuildingTypeID()) {
            case 0:
                break;
            case 1:
                Factory currentBuilding = this.gameObject.AddComponent<Factory>();
                currentBuilding.mainBulletpoint = this;
                break;
        }

        hasBuildingSprite = true;
    }

    /*
     * Getters & setters
     */

    public void setBuilding(Building building) {
        this.building = building;
    }

    public Building getBuilding() {
        return building;
    }

}
