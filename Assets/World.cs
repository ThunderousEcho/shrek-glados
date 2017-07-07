using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    float panSpeed = 1;

    public ParticleSystem stars;

	void Start () {
		
	}
	
	void Update () {
        var m = stars.main;
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
