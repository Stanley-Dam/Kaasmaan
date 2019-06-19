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

    private GameObject mainPlanet;

    // Start is called before the first frame update
    void Start() {
        
    }

    public bool IsWorkable() {
        bool isWorkable = false;

        switch (building.getBuildingTypeID()) {
            case 1:
                //FACTORY
                isWorkable = true;
                break;
            case 3:
                //MEADOW
                isWorkable = true;
                break;
            case 4:
                //RESEARCH
                isWorkable = true;
                break;
        }

        return isWorkable;
    }

    public int GetWorkingCivilianAmount() {
        int workingCivs = 0;

        //Getting civs amount on current building
        switch (building.getBuildingTypeID()) {
            case 0:
                //NONE
                break;
            case 1:
                //FACTORY
                Factory factory = this.gameObject.GetComponent<Factory>();
                workingCivs = factory.workingCivilianAmount;
                break;
            case 2:
                //HOUSING
                break;
            case 3:
                //MEADOW
                Meadow meadow = this.gameObject.GetComponent<Meadow>();
                workingCivs = meadow.workingCivilianAmount;
                break;
            case 4:
                //RESEARCH
                Research research = this.gameObject.GetComponent<Research>();
                workingCivs = research.workingCivilianAmount;
                break;
        }

        return workingCivs;
    }

    public void AddWorkingCiviliansToBuilding(int amount) {
        switch (building.getBuildingTypeID()) {
            case 0:
                //NONE
                break;
            case 1:
                //FACTORY
                Factory factory = this.gameObject.GetComponent<Factory>();
                factory.workingCivilianAmount += amount;
                break;
            case 2:
                //HOUSING
                break;
            case 3:
                //MEADOW
                Meadow meadow = this.gameObject.GetComponent<Meadow>();
                meadow.workingCivilianAmount += amount;
                break;
            case 4:
                //RESEARCH
                Research research = this.gameObject.GetComponent<Research>();
                research.workingCivilianAmount += amount;
                break;
        }
    }

    // Update is called once per frame
    void Update() {

        if (!Input.GetMouseButton(0) || !this.gameObject.GetComponent<BulletpointHover>().isInRange)
            return;

        if(ShopManager.buildingTypeOnMouse != null 
            && !hasBuildingSprite
            && !ShopManager.isMerging) {

            GameManager.amountOfCheese -= ShopManager.buildingTypeOnMouse.GetBuildingCost();

            this.building.setBuildingType(ShopManager.buildingTypeOnMouse);
            this.building.setLevel(1);

            UpdateBuilding();

            if (!(Input.GetKey(KeyCode.LeftShift) && GameManager.amountOfCheese > building.getBuildingType().GetBuildingCost()))
                ShopManager.hasPlaced();

            placedEffect = Instantiate(placedEffect, transform.position, Quaternion.identity);
            placedEffect.transform.SetParent(this.transform);
        } else if(hasBuildingSprite && ShopManager.buildingTypeOnMouse == null) {
            GameManager.selectedBulletpoint = this.gameObject;
        } else 
        if(hasBuildingSprite 
           && ShopManager.isMerging 
           && ShopManager.mergingBuildingLevel == building.getLevel()
           && ShopManager.buildingTypeOnMouse == building.getBuildingType()
           && ShopManager.mergeBuildingBulletpoint != this.gameObject) {

            //If the building is mergeable
            AddWorkingCiviliansToBuilding(ShopManager.mergeBuildingBulletpoint.GetComponent<MainBulletpoint>().GetWorkingCivilianAmount());
            if (building.getBuildingType().Equals(BuildingTypes.HOUSING)) this.gameObject.GetComponent<Housing>().LevelUp();
            Vector3 bulletpointPos = ShopManager.mergeBuildingBulletpoint.transform.localPosition;
            Destroy(ShopManager.mergeBuildingBulletpoint);

            mainPlanet = this.transform.parent.parent.gameObject;

            //Generate new bulletpoint
            GameObject currentObject = Instantiate(mainPlanet.GetComponent<BulletpointManager>().bulletpointPrefab);
            currentObject.transform.localPosition = bulletpointPos;
            currentObject.transform.SetParent(mainPlanet.GetComponent<BulletpointManager>().planetCenter.transform, false);

            //Rotate the bulletpoint
            Vector3 diff = mainPlanet.GetComponent<BulletpointManager>().planetCenter.localPosition - currentObject.transform.localPosition;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            currentObject.transform.localRotation = Quaternion.Euler(0f, 0f, rot_z + 90);

            this.building.LevelUp();
            UpdateBuilding();

            mainPlanet.GetComponent<BulletpointManager>().bulletPoints.Add(currentObject);
            mainPlanet.GetComponent<BulletpointManager>().buildingManager.CreateBuilding(0, currentObject, 0);

            GameManager.amountOfCheese -= building.getBuildingType().GetBuildingCost() * building.getLevel();
            ShopManager.hasPlaced();
        }
    }

    public void Salvage() {
        GameManager.amountOfWorkingCivilians -= GetWorkingCivilianAmount();
        if (building.getBuildingType().Equals(BuildingTypes.HOUSING)) GameManager.amountOfCivilians -= this.gameObject.GetComponent<Housing>().GetCivs();
        Vector3 bulletpointPos = this.transform.localPosition;

        mainPlanet = this.transform.parent.parent.gameObject;

        //Generate new bulletpoint
        GameObject currentObject = Instantiate(mainPlanet.GetComponent<BulletpointManager>().bulletpointPrefab);
        currentObject.transform.localPosition = bulletpointPos;
        currentObject.transform.SetParent(mainPlanet.GetComponent<BulletpointManager>().planetCenter.transform, false);

        //Rotate the bulletpoint
        Vector3 diff = mainPlanet.GetComponent<BulletpointManager>().planetCenter.localPosition - currentObject.transform.localPosition;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        currentObject.transform.localRotation = Quaternion.Euler(0f, 0f, rot_z + 90);

        mainPlanet.GetComponent<BulletpointManager>().bulletPoints.Add(currentObject);
        mainPlanet.GetComponent<BulletpointManager>().buildingManager.CreateBuilding(0, currentObject, 0);

        GameManager.amountOfCheese += this.building.getBuildingType().GetBuildingCost() * this.building.getLevel();

        ShopManager.hasPlaced();
        Destroy(this.gameObject);
    }

    public void UpdateBuilding() {

        //Setting the sprite of the building
        string spritePath = building.getBuildingType().getSpritePath();
        string spriteName = building.getBuildingType().getSpriteName();
        spriteName += BuildingTypes.LevelToStringTranslator(Mathf.CeilToInt((building.getLevel() / building.getBuildingType().getSpriteUpgradeDiv())));

        this.spriteRederer.sprite = Resources.Load<Sprite>(spritePath + spriteName);

        //Adding the building script to the building object
        switch(building.getBuildingTypeID()) {
            case 0:
                //NONE
                break;
            case 1:
                //FACTORY
                if(!this.gameObject.GetComponent<Factory>()) {
                    Factory factory = this.gameObject.AddComponent<Factory>();
                    factory.mainBulletpoint = this;
                    //Instantiates smoke for the level 1 factory
                    GameObject Smoke = Instantiate(smoke, transform.position, Quaternion.identity);
                    Smoke.transform.SetParent(factory.transform);
                    Smoke.transform.localPosition = new Vector3(0.2f, 0.8f);
                    Smoke.transform.rotation = transform.rotation;
                }

                break;
            case 2:
                //HOUSING
                if (!this.gameObject.GetComponent<Housing>()) {
                    Housing house = this.gameObject.AddComponent<Housing>();
                    house.mainBulletpoint = this;
                }

                if(building.getLevel() == 4) {
                    //Instantiates smoke for the level 1 house
                    GameObject Smoke = Instantiate(smoke, transform.position, Quaternion.identity);
                    Smoke.transform.SetParent(this.transform);
                    Smoke.transform.localPosition = new Vector3(0.2f, 0.5f);
                    Smoke.transform.rotation = transform.rotation;
                }

                break;
            case 3:
                //MEADOW
                if (!this.gameObject.GetComponent<Meadow>()) {
                    Meadow meadow = this.gameObject.AddComponent<Meadow>();
                    meadow.mainBulletpoint = this;
                }

                break;
            case 4:
                //RESEARCH
                if (!this.gameObject.GetComponent<Research>()) {
                    Research research = this.gameObject.AddComponent<Research>();
                    research.mainBulletpoint = this;
                }
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
