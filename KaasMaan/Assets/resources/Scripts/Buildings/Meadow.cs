using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meadow : MonoBehaviour {
    public float cooldownTickPerMiliSecond = 0.01f;
    private float generateCooldownTick = 0;
    public float amountOfMilkPerLevel = 2;
    public int workingCivilianAmount = 1;

    public MainBulletpoint mainBulletpoint;


    // Start is called before the first frame update
    void Start() {
        StartCoroutine(GenerateMilkCooldown());
    }

    // Update is called once per frame
    void Update() {
    }

    public IEnumerator GenerateMilkCooldown() {
        while (generateCooldownTick < 1) {
            generateCooldownTick += cooldownTickPerMiliSecond;
            yield return new WaitForSeconds(0.1f);
        }

        StopCoroutine(GenerateMilkCooldown());
        generateCooldownTick = 0;

        GameManager.amountOfMilk += (this.gameObject.GetComponent<MainBulletpoint>().getBuilding().getLevel() * workingCivilianAmount) * amountOfMilkPerLevel;
        StartCoroutine(GenerateMilkCooldown());
    }
}
