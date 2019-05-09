using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour {

    public Transform trackedObject;
    public Transform trackedObjectCenter;
    public Camera cam;

    public float extraSpacing = 3;
    public float growPerFrame = 0.1f;
    public float startZoomSize = 10;
    public float maxZoomSize = 9;

    private bool isZoomed = false;
    private float prevSize = 0;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        zoomTick();
        cameraMovement();
    }

    /*
     * Make the player able to move around the planet with the camera if it the camera has been zoomed in.
     */

    private void cameraMovement() {
        if (!isZoomed) return;

        if(Input.GetKey(KeyCode.A)) {
            trackedObjectCenter.Rotate(new Vector3(0, 0, 0.1f));
        }

        if (Input.GetKey(KeyCode.D)) {
            trackedObjectCenter.Rotate(new Vector3(0, 0, -0.1f));
        }

    }

    /*
     * Update the camera position / size
     */

    private void zoomTick() {
        //If the camera is to close to the planet
        if (trackedObject.localScale.x + extraSpacing > cam.orthographicSize && !isZoomed) {
            cam.orthographicSize += growPerFrame;
        }

        //When the planet gets to a size where you'll have to zoom
        if (trackedObject.localScale.x + extraSpacing >= startZoomSize && trackedObject.localScale.x > prevSize) {

            cam.orthographicSize = maxZoomSize;

            //Zoom towards a point
            transform.parent = null;
            transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 0));
            transform.position = new Vector3(0, trackedObject.localScale.y * 1.2f, this.transform.position.z);
            transform.parent = trackedObjectCenter;

            prevSize = trackedObject.localScale.x;
            isZoomed = true;
        }
    }

}
