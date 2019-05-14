using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    //The arraylist to keep track of all the buildings in the game
    private ArrayList buildings = new ArrayList();

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    //Add a building to the array
    public void CreateBuilding(int buildingType, GameObject bulletpoint, int level) {
        Building newBuilding = new Building(buildings.Count, buildingType, bulletpoint, level);
        bulletpoint.GetComponent<MainBulletpoint>().setBuilding(newBuilding);
        buildings.Add(newBuilding);
    }

    //Returns a buildig with from a buildingID
    public Building getBuilding(int buildingID) {
        foreach(Building currentBuilding in buildings) {
            if (currentBuilding.getBuildingID() == buildingID) return currentBuilding;
        }
        return null;
    }

    //Returns a buildig with from a bulletpoint
    public Building getBuilding(GameObject bulletpoint) {
        foreach (Building currentBuilding in buildings) {
            if (currentBuilding.getBulletpoint() == bulletpoint) return currentBuilding;
        }
        return null;
    }

    //Returns all buildings of a specific type
    public Building[] getBuildingsOfType(int buildingType) {

        List<Building> buildingsList = new List<Building>();

        int index = 0;
        foreach (Building currentBuilding in buildings) {
            if (currentBuilding.getBuildingType() == buildingType) {
                buildingsList[index] = currentBuilding;
                index++;
            }
        }

        Building[] buildingsToReturn = buildingsList.ToArray();
        return buildingsToReturn;
    }

}
