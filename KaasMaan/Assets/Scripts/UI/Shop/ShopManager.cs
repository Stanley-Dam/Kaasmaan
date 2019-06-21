using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {

    public Transform planetCenter;
    public GameObject shopMenu;
    public GameObject equipPrefab;
    public List<GameObject> createdObjects = new List<GameObject>();

    public static BuildingTypes buildingTypeOnMouse;
    public static int mergingBuildingLevel = 0;
    public static bool isMerging = false;
    public static GameObject mergeBuildingBulletpoint;

    private float minX, maxX, minY, maxY;
    private Vector3 position;
    private GameObject go;

    void Start() {
        if (shopMenu.active == true) {
            shopMenu.active = false;
        }

        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = bottomCorner.x;
        maxX = topCorner.x;
        minY = bottomCorner.y;
        maxY = topCorner.y;

    }

    private void Update() {
        DeselectedBuilding();
    }

    public void OpenShop() {
        if (shopMenu.active == true) {
            shopMenu.active = false;
        }
        else {
            shopMenu.active = true;
        }
    }

    public void SelectedBuilding(int buildingID) {
        //Check if we can pay for this building
        if (BuildingTypes.getBuildingFromTypeID(buildingID).GetBuildingCost() > GameManager.amountOfCheese)
            return;

        //Equip the building
        if (equipPrefab != null) {
            if (createdObjects.Count != 1) {
                position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -3f);

                go = (GameObject)Instantiate(equipPrefab, position, Quaternion.identity);

                string sprite = BuildingTypes.getBuildingFromTypeID(buildingID).getSpritePath() + BuildingTypes.getBuildingFromTypeID(buildingID).getSpriteName() + "01";

                go.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(sprite);
                buildingTypeOnMouse = BuildingTypes.getBuildingFromTypeID(buildingID);
                createdObjects.Add(go);

                go.GetComponent<FollowMouse>().planetCenter = planetCenter;
            }
        }

    }

    public void SelectedBuildingMerging() {

        //Equip the building
        if (equipPrefab != null) {
            if (createdObjects.Count != 1) {
                Building building = GameManager.selectedBulletpoint.GetComponent<MainBulletpoint>().getBuilding();
                int buildingLevel = GameManager.selectedBulletpoint.GetComponent<MainBulletpoint>().getBuilding().getLevel();
                mergeBuildingBulletpoint = GameManager.selectedBulletpoint;

                position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -3f);

                go = (GameObject)Instantiate(equipPrefab, position, Quaternion.identity);

                string levelAsString = "" + buildingLevel;
                if (buildingLevel < 10) levelAsString = "0" + buildingLevel;

                string sprite = building.getBuildingType().getSpritePath() + building.getBuildingType().getSpriteName() + levelAsString;

                go.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(sprite);
                buildingTypeOnMouse = building.getBuildingType();
                createdObjects.Add(go);

                go.GetComponent<FollowMouse>().planetCenter = planetCenter;
                mergingBuildingLevel = buildingLevel;
                isMerging = true;
            }
        }

    }

    public static void hasPlaced() {
        buildingTypeOnMouse = null;
        mergingBuildingLevel = 0;
        isMerging = false;
        mergeBuildingBulletpoint = null;
    }

    public void DeselectedBuilding() {
        if ((Input.GetMouseButtonDown(1) || buildingTypeOnMouse == null) && createdObjects.Capacity > 0) {
            Destroy(go);
            createdObjects.Clear();
            buildingTypeOnMouse = null;
            mergingBuildingLevel = 0;
            isMerging = false;
            mergeBuildingBulletpoint = null;
        }
    }
}
