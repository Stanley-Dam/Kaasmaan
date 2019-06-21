using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {

    public float cooldownTickPerMiliSecond = 0.05f;
    private float generateCooldownTick = 0;
    public float amountOfCheasePerLevel = 5;
    public int workingCivilianAmount = 0;

    public MainBulletpoint mainBulletpoint;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(GenerateCheeseCooldown());
    }

    // Update is called once per frame
    void Update() {
    }

    public IEnumerator GenerateCheeseCooldown() {
        while (generateCooldownTick < 1) {
            generateCooldownTick += cooldownTickPerMiliSecond;
            yield return new WaitForSeconds(0.1f);
        }

        StopCoroutine(GenerateCheeseCooldown());
        generateCooldownTick = 0;
        GenerateTheCheese();
    }

    private void GenerateTheCheese() {
        bool canPay = false;
        float cost = (this.gameObject.GetComponent<MainBulletpoint>().getBuilding().getLevel() * workingCivilianAmount);

        while (!canPay) {
            canPay = (GameManager.amountOfMilk >= cost * Mathf.Ceil(amountOfCheasePerLevel / 3));
            if (!canPay) cost--;
            if (cost <= 0) {
                StartCoroutine(GenerateCheeseCooldown());
                return;
            }
        }

        GameManager.amountOfMilk -= cost * Mathf.Ceil(amountOfCheasePerLevel / 3);
        GameManager.amountOfCheese += cost * amountOfCheasePerLevel;
        StartCoroutine(GenerateCheeseCooldown());
    }

}
