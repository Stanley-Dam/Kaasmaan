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

    public static GameObject selectedBuilding;

    [SerializeField] private Image progressBar;
    [SerializeField] private Text amountOfMilkUI;
    [SerializeField] private Text amountOfCheeseUI;
    [SerializeField] private Text amountOfCiviliansUI;

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
    }
}
