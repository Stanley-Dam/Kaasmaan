using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletpointHover : MonoBehaviour {

    public float mouseHoverDistance = 0.5f;
    public float normalBulletpointSize = 0.3f;
    public float zoomedBulletpointSize = 1f;
    public float zoomPerMicroSecond = 0.1f;

    private bool isPlayerAnimation = false;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        //Check if the mouse is close enough to play the animation
        if (Vector2.Distance(mousePosition, this.transform.position) < mouseHoverDistance) {
            startAnimation(true);
        } else if(this.transform.localScale.x > 0.3f) {
            startAnimation(false);
        }

    }

    private void startAnimation(bool zoomIn) {
        if (!isPlayerAnimation && zoomIn) {
            isPlayerAnimation = true;
            StartCoroutine(doAnimation());
        } else if(!isPlayerAnimation) {
            isPlayerAnimation = true;
            StartCoroutine(undoAnimation());
        }
    }

    public IEnumerator doAnimation() {
        while (this.transform.localScale.x < zoomedBulletpointSize) {

            this.transform.localScale += new Vector3(zoomPerMicroSecond, zoomPerMicroSecond);
            yield return new WaitForSeconds(0.01f);

        }

        StopCoroutine(doAnimation());
        isPlayerAnimation = false;
    }

    public IEnumerator undoAnimation() {
        while (this.transform.localScale.x >= normalBulletpointSize) {

            this.transform.localScale -= new Vector3(zoomPerMicroSecond, zoomPerMicroSecond);
            yield return new WaitForSeconds(0.01f);

        }

        StopCoroutine(undoAnimation());
        isPlayerAnimation = false;
    }

}
