using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {

    public float cooldownTickPerMiliSecond = 0.01f;
    private float generateCooldownTick = 0;
    public float amountOfCheasePerLevel = 1;

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
        GameManager.amountOfCheese += this.gameObject.GetComponent<MainBulletpoint>().getBuilding().getLevel() * amountOfCheasePerLevel;
        StartCoroutine(GenerateCheeseCooldown());
    }

}
