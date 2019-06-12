using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBulletpoint : MonoBehaviour {

    public SpriteRenderer spriteRederer;

    private Building building;
    private bool hasBuildingSprite = false;

    public GameObject smoke;
    public GameObject placedEffect;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButton(0) && this.gameObject.GetComponent<BulletpointHover>().isInRange && ShopManager.buildingTypeOnMouse != null && !hasBuildingSprite) {
            GameManager.amountOfCheese -= ShopManager.buildingTypeOnMouse.GetBuildingCost();

            this.building.setBuildingType(ShopManager.buildingTypeOnMouse);
            this.building.setLevel(1);

            UpdateBuilding();
            ShopManager.hasPlaced();

            placedEffect = Instantiate(placedEffect, transform.position, Quaternion.identity);
            placedEffect.transform.SetParent(this.transform);
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
                //NONE
                break;
            case 1:
                //FACTORY
                Factory factory = this.gameObject.AddComponent<Factory>();
                factory.mainBulletpoint = this;
                //Instantiates smoke for the level 1 factory
                GameObject Smoke = Instantiate(smoke, transform.position, Quaternion.identity);
                Smoke.transform.SetParent(factory.transform);
                Smoke.transform.localPosition = new Vector3(0, 1);
                Smoke.transform.rotation = transform.rotation;
                break;
            case 2:
                //HOUSING
                Smoke = Instantiate(smoke, transform.position, Quaternion.identity);
                Smoke.transform.SetParent(this.transform);
                Smoke.transform.localPosition = new Vector3(0.2f, 0.5f);
                Smoke.transform.rotation = transform.rotation;
                break;
            case 3:
                //MEADOW
                Meadow meadow = this.gameObject.AddComponent<Meadow>();
                meadow.mainBulletpoint = this;
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
