using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineEnemy : MonoBehaviour {

    float health = 3;

    public SpriteRenderer rend;

    float deathTimer = -1;

    bool f = false;

    public AudioClip death;

	void Start () {
		
	}
	
	void Update () {

        if (deathTimer > 0.0625f) {
            Destroy(gameObject);
        } else if (health <= 0) {
            deathTimer += Time.deltaTime;
            return;
        }

        Vector3 n = transform.position;
        n.y = Mathf.Sin(Time.time) * 5;
        transform.LookAt(transform.position + Vector3.forward, n + Vector3.right * Time.deltaTime - transform.position);
        transform.position = n;

        if (!f) {
            transform.Translate(-Vector3.forward * transform.position.z);
        }
        f = false;
    }

    public void damage(float amount) {
        if (health <= 0)
            return;

        health -= amount;
        rend.color = Color.Lerp(Color.white, Color.red, (1-health / 3));
        f = true;

        if (health <= 0) {
            Vector3 t = transform.position;
            t.z = -7;
            transform.position = t; //make visible above peek
            rend.color = Color.red;
            AudioSource.PlayClipAtPoint(death, Camera.main.transform.position);
            deathTimer = 0;
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
