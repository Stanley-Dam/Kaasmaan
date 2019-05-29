using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {

    public float cooldownTickPerMiliSecond = 0.01f;
    private float generateCooldownTick = 0;
    public float amountOfCheasePerLevel = 1;

    public MainBulletpoint mainBulletpoint;


    // Start is called before the first frame update
    void Start() {
        StartCoroutine(GenerateCheeseCooldown());
        GameObject Smoke = Instantiate(mainBulletpoint.smoke, transform.position, Quaternion.identity);
        Smoke.transform.SetParent(this.transform);
        Smoke.transform.localPosition = new Vector3(0, 1);
        Smoke.transform.rotation = transform.rotation;
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
