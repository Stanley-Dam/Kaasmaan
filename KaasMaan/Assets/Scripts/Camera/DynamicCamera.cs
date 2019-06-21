using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour {

    public Transform trackedObject;
    public Transform trackedObjectCenter;
    public Camera cam;

    public float speed = 30f;
    public float fastSpeed = 60f;
    public float extraSpacing = 3;
    public float growPerFrame = 0.1f;
    public float startZoomSize = 10;
    public float maxZoomSize = 9;

    public Transform background;
    public Transform buildingViewer;

    private bool isZoomed = false;
    private float prevSize = 0;

    private float bulletpointsDistance = 0.75f;

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

        float speed = this.speed;

        if (Input.GetKey(KeyCode.LeftControl))
            speed = fastSpeed;

        if(Input.GetKey(KeyCode.A)) {
            trackedObjectCenter.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.D)) {
            trackedObjectCenter.Rotate(new Vector3(0, 0, -speed * Time.deltaTime));
        }

    }

    /*
     * Update the camera position / size
     */

    private void zoomTick() {
        //If the camera is too close to the planet
        if (trackedObject.localScale.x + extraSpacing > cam.orthographicSize && !isZoomed) {
            cam.orthographicSize += growPerFrame;
        }

        background.localScale = new Vector3(cam.orthographicSize/3, cam.orthographicSize/3);
        buildingViewer.localScale = new Vector3(cam.orthographicSize / 3, cam.orthographicSize / 3);
        buildingViewer.localPosition = new Vector3(-(cam.orthographicSize) * 0.6f, -(cam.orthographicSize * 0.95f), 1);

        //When the planet gets to a size where you'll have to zoom
        if (trackedObject.localScale.x + extraSpacing >= startZoomSize && trackedObject.localScale.x > prevSize) {

            cam.orthographicSize = maxZoomSize;

            //Zoom towards a point
            transform.parent = null;
            transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 0));
            transform.position = new Vector3(0, trackedObject.localScale.y * bulletpointsDistance, this.transform.position.z);
            transform.parent = trackedObjectCenter;

            prevSize = trackedObject.localScale.x;
            isZoomed = true;
        }

    }

}
