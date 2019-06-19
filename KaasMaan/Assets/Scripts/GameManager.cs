using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static float amountOfCheese = 640;
    public static float amountOfMilk = 0;
    public static int amountOfCivilians = 0;
    public static int amountOfWorkingCivilians = 0;
    public static int planetLevel = 1;

    public static GameObject selectedBulletpoint;

    [SerializeField] private Image progressBar;
    [SerializeField] private Text amountOfMilkUI;
    [SerializeField] private Text amountOfCheeseUI;
    [SerializeField] private Text amountOfCiviliansUI;

    [SerializeField] private GameObject buildingSettings;
    [SerializeField] private GameObject buildingWorkingSettings;
    [SerializeField] private Text workingCiviliansAtBuilding;
    [SerializeField] private GameObject currentBuildingSprite;

    [SerializeField] private GameObject ExpandButton;

    private float level = 0;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        //Check if there are enough civs to provide for all the buildings
        if(amountOfWorkingCivilians > amountOfCivilians) {
            foreach(GameObject bulletPoint in this.gameObject.GetComponent<BulletpointManager>().bulletPoints) {
                if (bulletPoint == null) continue;
                if (bulletPoint.GetComponent<MainBulletpoint>().IsWorkable()) {
                    amountOfWorkingCivilians -= bulletPoint.GetComponent<MainBulletpoint>().GetWorkingCivilianAmount();
                    bulletPoint.GetComponent<MainBulletpoint>().AddWorkingCiviliansToBuilding(-bulletPoint.GetComponent<MainBulletpoint>().GetWorkingCivilianAmount());
                }
                if (amountOfWorkingCivilians <= amountOfCivilians) break;
            }
        }

        //Update the progressBar
        progressBar.fillAmount = amountOfCheese / (planetLevel * (1000 * planetLevel));

        ExpandButton.active = amountOfCheese >= (planetLevel * (1000 * planetLevel));

        //Update the UI
        amountOfMilkUI.text = "" + amountOfMilk;
        amountOfCheeseUI.text = "" + amountOfCheese;
        amountOfCiviliansUI.text = "" + amountOfCivilians;

        //Update the building menu
        buildingSettings.active = (selectedBulletpoint != null);
        buildingWorkingSettings.active = buildingSettings.active && selectedBulletpoint.GetComponent<MainBulletpoint>().IsWorkable();
        currentBuildingSprite.active = buildingSettings.active;

        if(buildingSettings.active)
            currentBuildingSprite.GetComponent<SpriteRenderer>().sprite = selectedBulletpoint.GetComponent<SpriteRenderer>().sprite;

        if (buildingWorkingSettings.active) {
            workingCiviliansAtBuilding.text = "" + selectedBulletpoint.GetComponent<MainBulletpoint>().GetWorkingCivilianAmount() + " / " + (amountOfCivilians - amountOfWorkingCivilians);
        }

    }

    public void Salvage() {
        selectedBulletpoint.GetComponent<MainBulletpoint>().Salvage();
    }

    public void AddCivsToSelectedBuilding() {
        if (amountOfCivilians - amountOfWorkingCivilians < 1)
            return;

        selectedBulletpoint.GetComponent<MainBulletpoint>().AddWorkingCiviliansToBuilding(1);
        amountOfWorkingCivilians++;
    }

    public void RemoveCivsFromSelectedBuilding() {
        if (selectedBulletpoint.GetComponent<MainBulletpoint>().GetWorkingCivilianAmount() < 1)
            return;

        selectedBulletpoint.GetComponent<MainBulletpoint>().AddWorkingCiviliansToBuilding(-1);
        amountOfWorkingCivilians--;
    }
}
