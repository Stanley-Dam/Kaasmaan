using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    private int buttonAmount;
    private int indexInButtonList = 0;

    // Start is called before the first frame update
    void Start() {
        buttonAmount = this.transform.childCount;
        StartCoroutine(SpawnItems());
    }

    // Update is called once per frame
    void Update() {
        
    }

    public IEnumerator SpawnItems() {
        while (indexInButtonList < buttonAmount) {
            this.transform.GetChild(indexInButtonList).gameObject.SetActive(true);
            indexInButtonList++;
            yield return new WaitForSeconds(0.1f);
        }

        StopCoroutine(SpawnItems());
    }

}
