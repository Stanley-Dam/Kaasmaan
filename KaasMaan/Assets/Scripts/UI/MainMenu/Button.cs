using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

    [SerializeField] private int buttonType;

    public void OnClick() {
        switch(buttonType) {
            case 0:
                SceneManager.LoadScene(1);
                break;
            case 2:
                Application.Quit();
                break;
            case 3:
                SceneManager.LoadScene(0);
                break;
        }
    }
}
