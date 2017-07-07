using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineEnemy : MonoBehaviour {

    int health = 5;

	void Start () {
		
	}
	
	void Update () {
        Vector3 f = transform.position;
        f.y = Mathf.Sin(Time.time) * 5;
        f.z = 0;

        transform.LookAt(transform.position + Vector3.forward, f + Vector3.right * Time.deltaTime - transform.position);

        transform.position = f;
	}

    public void damage(int amount) {
        health -= amount;
        if (health < 0)
            Destroy(gameObject);
    }
}
