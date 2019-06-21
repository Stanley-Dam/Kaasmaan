using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Research : MonoBehaviour {

    public float cooldownTickPerMiliSecond = 0.05f;
    private float generateCooldownTick = 0;
    public int workingCivilianAmount = 0;

    public MainBulletpoint mainBulletpoint;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    public IEnumerator GenerateResearchCooldown() {
        while (generateCooldownTick < 1) {
            generateCooldownTick += cooldownTickPerMiliSecond;
            yield return new WaitForSeconds(0.1f);
        }

        StopCoroutine(GenerateResearchCooldown());
        GameManager.researchAmount += workingCivilianAmount;
        generateCooldownTick = 0;
        StartCoroutine(GenerateResearchCooldown());
    }

}
