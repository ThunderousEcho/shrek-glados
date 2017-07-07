using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public static float panSpeed = 1;

    public ParticleSystem stars;

    public World main;

    float transitionStartTime = -128;

	void Start () {
        main = this;
	}
	
	void Update () {
        var m = stars.main;

        if (Time.time > transitionStartTime + 2) {
            transitionStartTime = -128;
            panSpeed = 1;
        } else if (transitionStartTime > -64) {
            panSpeed = 1 + (1 - Mathf.Cos((Time.time - transitionStartTime) * Mathf.PI)) * 2;
        }

        m.simulationSpeed = panSpeed * 5;
        ParticleSystemRenderer r = stars.GetComponent<ParticleSystemRenderer>();
        r.lengthScale = panSpeed;

        Camera.main.orthographicSize = 5;
        float desiredWidth = 5 * 16f / 9f;
        float aspect = Screen.width / (float)Screen.height;
        float currentWidth = aspect * 5;

        if (desiredWidth > currentWidth) {
            Camera.main.orthographicSize *= desiredWidth / currentWidth;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            transition();
        }
    }

    public void transition() {
        transitionStartTime = Time.time;
    }
}
