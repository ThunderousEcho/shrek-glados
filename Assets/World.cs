using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public static float panSpeed = 1;

    public ParticleSystem stars;

    public World main;

    public static float transitionStartTime = -128;

    public GameObject[] levels;
    int currentLevel = 0;
    Vector3 p;

    void Start() {
        main = this;
    }

    void Update() {

        if (levels[currentLevel].transform.childCount == 0) {
            if (currentLevel < levels.Length -1) {
                currentLevel++;
                transitionStartTime = Time.time;
                levels[currentLevel].SetActive(true);
                p = levels[currentLevel].transform.position;
            } else {
                Debug.Log("You Win!");
            }
        }

        var m = stars.main;

        if (Time.time > transitionStartTime + 2) {
            transitionStartTime = -128;
            panSpeed = 1;
            levels[currentLevel].transform.position = Vector3.zero;
        } else if (transitionStartTime > -64) {
            panSpeed = 1 + (1 - Mathf.Cos((Time.time - transitionStartTime) * Mathf.PI)) * 2;

            if (Time.time > transitionStartTime + 1) {
                float j = Time.time - transitionStartTime - 1;
                levels[currentLevel].transform.position = Vector3.Lerp(levels[currentLevel].transform.position, Vector3.zero, Time.deltaTime * 10);
            }
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
    }
}
