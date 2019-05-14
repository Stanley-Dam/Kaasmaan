using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBulletpoint : MonoBehaviour {

    private Building building;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
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
