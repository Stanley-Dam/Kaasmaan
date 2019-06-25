using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInfoScreen : MonoBehaviour{
    public GameObject infoScreen;
    public GameObject shopButton;
    public GameObject shopPanel;
    public GameObject BuildingSetting;
    public GameObject progressBar;
    public GameObject playerStats;
    public GameObject progressBarRotten;
    public GameObject progressBarBH;
    public GameObject rottenStats;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void openInfoScreen() {
        if (infoScreen.active == true) {
            infoScreen.active = false;
        } else {
            infoScreen.active = true;
        }

    }

    public void InfoScreenOpened() {
        if (infoScreen.active == true) {
            shopButton.active = false;
            shopPanel.active = false;
            BuildingSetting.active = false;
            progressBar.active = false;
            playerStats.active = false;
            progressBarRotten.active = false;
            progressBarBH.active = false;
            rottenStats.active = false;
        }

        if (infoScreen.active == false) {
            shopButton.active = true;
            shopPanel.active = true;
            BuildingSetting.active = true;
            progressBar.active = true;
            playerStats.active = true;
            progressBarRotten.active = true;
            progressBarBH.active = true;
            rottenStats.active = true;
        }
    }
}
