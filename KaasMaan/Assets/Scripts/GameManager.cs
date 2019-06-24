using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static float amountOfCheese = 640;
    public static float amountOfMilk = 0;
    public static int amountOfCivilians = 0;
    public static int amountOfWorkingCivilians = 0;
    public static int planetLevel = 1;

    public static float blackHoleLevel = 0.001f;
    public static float blackHoleLevelToPlanet = 0;

    public static float researchAmount = 0;
    public static float researchFinish = 1000000;

    public static GameObject selectedBulletpoint;

    [SerializeField] private Text amountOfMilkUI;
    [SerializeField] private Text amountOfCheeseUI;
    [SerializeField] private Text amountOfCiviliansUI;

    [SerializeField] private Image progressBar;
    [SerializeField] private Image progressBarRotten;
    [SerializeField] private Image blackHoleBar;

    [SerializeField] private GameObject buildingSettings;
    [SerializeField] private GameObject buildingWorkingSettings;
    [SerializeField] private Text workingCiviliansAtBuilding;
    [SerializeField] private GameObject currentBuildingSprite;

    [SerializeField] private GameObject ExpandButton;

    private float level = 0;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(UpgradeBlackHole());
    }

    // Update is called once per frame
    void Update() {
        UpdateBlackHole();

        //If player wants to stop playing
        if (Input.GetKey(KeyCode.Escape))
            OnGameOver(0);

        //Check if there are enough civs to provide for all the buildings
        if (amountOfWorkingCivilians > amountOfCivilians) {
            foreach(GameObject bulletPoint in this.gameObject.GetComponent<BulletpointManager>().bulletPoints) {
                if (bulletPoint == null) continue;
                if (bulletPoint.GetComponent<MainBulletpoint>().IsWorkable()) {
                    amountOfWorkingCivilians -= bulletPoint.GetComponent<MainBulletpoint>().GetWorkingCivilianAmount();
                    bulletPoint.GetComponent<MainBulletpoint>().AddWorkingCiviliansToBuilding(-bulletPoint.GetComponent<MainBulletpoint>().GetWorkingCivilianAmount());
                }
                if (amountOfWorkingCivilians <= amountOfCivilians) break;
            }
        }

        //Update the progressBars
        progressBar.fillAmount = amountOfCheese / (planetLevel * (1000 * planetLevel));
        progressBarRotten.fillAmount = researchAmount / researchFinish;
        blackHoleBar.fillAmount = blackHoleLevel / planetLevel;

        ExpandButton.active = amountOfCheese >= (planetLevel * (1000 * planetLevel));

        //Update the UI
        amountOfMilkUI.text = "" + NumberToString.GetString(amountOfMilk);
        amountOfCheeseUI.text = "" + NumberToString.GetString(amountOfCheese);
        amountOfCiviliansUI.text = "" + NumberToString.GetString(amountOfCivilians);

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

    private void OnGameOver(int sceneTo) {
        amountOfCheese = 640;
        amountOfMilk = 0;
        amountOfCivilians = 0;
        amountOfWorkingCivilians = 0;
        planetLevel = 1;

        blackHoleLevel = 0.001f;
        blackHoleLevelToPlanet = 0;

        researchAmount = 0;
        researchFinish = 10000;

        selectedBulletpoint = null;

        SceneManager.LoadScene(sceneTo);
    }

    public void Salvage() {
        selectedBulletpoint.GetComponent<MainBulletpoint>().Salvage();
    }

    public void AddCivsToSelectedBuilding() {
        int amount = 1;
        if(Input.GetKey(KeyCode.LeftShift)) {
            amount = (selectedBulletpoint.GetComponent<MainBulletpoint>().getBuilding().getLevel() * 5) - selectedBulletpoint.GetComponent<MainBulletpoint>().GetWorkingCivilianAmount();
            if (amountOfCivilians - amountOfWorkingCivilians < amount)
                amount -= amount - (amountOfCivilians - amountOfWorkingCivilians);
        }

        if (amountOfCivilians - amountOfWorkingCivilians < amount)
            return;

        if(selectedBulletpoint.GetComponent<MainBulletpoint>().AddWorkingCiviliansToBuilding(amount))
            amountOfWorkingCivilians += amount;
    }

    public void RemoveCivsFromSelectedBuilding() {
        int amount = 1;
        if (Input.GetKey(KeyCode.LeftShift)) {
            amount = selectedBulletpoint.GetComponent<MainBulletpoint>().GetWorkingCivilianAmount();
        }

        if (selectedBulletpoint.GetComponent<MainBulletpoint>().GetWorkingCivilianAmount() < amount)
            return;

        selectedBulletpoint.GetComponent<MainBulletpoint>().AddWorkingCiviliansToBuilding(-amount);
        amountOfWorkingCivilians -= amount;
    }

    private float generateCooldownTick = 0;
    private float cooldownTickPerMiliSecond = 0.02f;
    private float increaseSize = 0.025f;

    private void UpdateBlackHole() {
        if(amountOfCheese > 0)
            amountOfCheese -= Mathf.Floor(blackHoleLevel * Time.deltaTime);
        if (blackHoleLevel >= planetLevel)
            OnGameOver(2);
    }

    public IEnumerator UpgradeBlackHole() {
        while (generateCooldownTick < 1) {
            generateCooldownTick += cooldownTickPerMiliSecond;
            yield return new WaitForSeconds(0.1f);
        }

        StopCoroutine(UpgradeBlackHole());
        generateCooldownTick = 0;
        blackHoleLevel += increaseSize;
        increaseSize *= 1.01f;
        cooldownTickPerMiliSecond *= 1.001f;
        StartCoroutine(UpgradeBlackHole());
    }

}
