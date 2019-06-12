using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static float amountOfCheese = 900;
    public static float amountOfMilk = 10;
    public static int amountOfCivilians = 0;
    public static int amountOfWorkingCivilians = 0;
    public static int planetLevel = 1;

    public static GameObject selectedBulletpoint;

    [SerializeField] private Image progressBar;
    [SerializeField] private Text amountOfMilkUI;
    [SerializeField] private Text amountOfCheeseUI;
    [SerializeField] private Text amountOfCiviliansUI;

    [SerializeField] private GameObject buildingSettings;
    [SerializeField] private Text workingCiviliansAtBuilding;

    private float level = 0;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        //Update the progressBar
        progressBar.fillAmount = amountOfCheese / (planetLevel * 1000);

        //Update the UI
        amountOfMilkUI.text = "" + amountOfMilk;
        amountOfCheeseUI.text = "" + amountOfCheese;
        amountOfCiviliansUI.text = "" + amountOfCivilians;

        //Update the building menu
        buildingSettings.active = (selectedBulletpoint != null && selectedBulletpoint.GetComponent<MainBulletpoint>().IsWorkable());

        if(buildingSettings.active) {
            workingCiviliansAtBuilding.text = "" + selectedBulletpoint.GetComponent<MainBulletpoint>().GetWorkingCivilianAmount() + " / " + (amountOfCivilians - amountOfWorkingCivilians);
        }
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
