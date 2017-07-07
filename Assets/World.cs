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
    }
}
