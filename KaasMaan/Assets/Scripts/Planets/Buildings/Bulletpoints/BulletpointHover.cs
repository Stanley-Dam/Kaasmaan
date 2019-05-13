using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletpointHover : MonoBehaviour {

    public AudioSource audioSource;

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
        } else if(this.transform.localScale.x > normalBulletpointSize) {
            startAnimation(false);
        }

    }

    /*
     * Animation
     */

    private void startAnimation(bool zoomIn) {
        if (!isPlayerAnimation && zoomIn && this.transform.localScale.x < zoomedBulletpointSize) {
            isPlayerAnimation = true;
            StartCoroutine(doAnimation());
            playClickFX();
        } else if(!isPlayerAnimation && !zoomIn) {
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
        while (this.transform.localScale.x > normalBulletpointSize) {

            this.transform.localScale -= new Vector3(zoomPerMicroSecond, zoomPerMicroSecond);
            yield return new WaitForSeconds(0.01f);

        }

        StopCoroutine(undoAnimation());
        isPlayerAnimation = false;
    }

    /*
     * Audio players
     */

    private void playClickFX() {
        AudioClip clip = (AudioClip)Resources.Load("Audio/Buildings/Bulletpoint/HoverClickFX");
        audioSource.PlayOneShot(clip);
    }

}
