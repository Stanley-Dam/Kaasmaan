using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBulletpoint : MonoBehaviour {

    public SpriteRenderer spriteRederer;

    private Building building;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButton(0) && this.gameObject.GetComponent<BulletpointHover>().isInRange) {
            this.building.setBuildingType(BuildingTypes.FACTORY);
            this.building.setLevel(1);

            string spritePath = building.getBuildingType().getSpritePath();
            string spriteName = building.getBuildingType().getSpriteName();
            spriteName += BuildingTypes.LevelToStringTranslator(Mathf.CeilToInt((building.getLevel()/building.getBuildingType().getSpriteUpgradeDiv())+1));

            Debug.Log(spritePath + spriteName);
            this.spriteRederer.sprite = Resources.Load<Sprite>(spritePath + spriteName);
        }
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
