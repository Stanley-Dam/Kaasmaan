using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    GameObject currentHover;

    [SerializeField] private int typeID;
    [SerializeField] private Texture costSprite;
    [SerializeField] private bool mergeButton = false;

    void OnGUI() {
        if (currentHover == this.gameObject) {
            //Give the player information!
            GUI.Label(new Rect(new Vector2(Input.mousePosition.x - 164, Screen.height - (Input.mousePosition.y + 50)), new Vector2(203, 119)), costSprite);

            float cost = BuildingTypes.getBuildingFromTypeID(typeID).GetBuildingCost();

            if (mergeButton)
                cost = (GameManager.selectedBulletpoint.GetComponent<MainBulletpoint>().getBuilding().getLevel() * GameManager.selectedBulletpoint.GetComponent<MainBulletpoint>().getBuilding().getBuildingType().GetBuildingCost()) * 2;


            GUIStyle style = new GUIStyle();
            style.fontSize = 20;
            style.normal.textColor = Color.white;
            if (cost > GameManager.amountOfCheese)
                style.normal.textColor = Color.red;
            
            GUI.Label(new Rect(new Vector2(Input.mousePosition.x - 85, Screen.height - Input.mousePosition.y), new Vector2(203, 119)),
                                            "" + cost,
                                            style);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (eventData.pointerCurrentRaycast.gameObject != null) {
            currentHover = eventData.pointerCurrentRaycast.gameObject;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        currentHover = null;
    }

}
