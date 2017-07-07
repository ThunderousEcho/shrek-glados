using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    protected float maxHealth = 0.5f;
    protected float health = 0.5f;

    public SpriteRenderer rend;

    float deathTimer = -1;

    bool f = false;

    public AudioClip death;

	public virtual void Start () {
        
    }
	
	public virtual void Update () {

        if (World.transitionStartTime > -64)
            return;

        if (deathTimer > 0.0625f) {
            Destroy(gameObject);
        } else if (health <= 0) {
            deathTimer += Time.deltaTime;
            return;
        }

        Vector3 n = getPosition(transform.position);
        transform.LookAt(transform.position + Vector3.forward, n + Vector3.right * Time.deltaTime * World.panSpeed - transform.position);
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
        rend.color = Color.Lerp(Color.white, Color.red, (1-health / maxHealth));
        f = true;

        if (health <= 0) {
            Vector3 t = transform.position;
            t.z = -7;
            transform.position = t; //make visible above peek
            rend.color = Color.red;
            AudioSource.PlayClipAtPoint(death, Camera.main.transform.position);
            deathTimer = 0;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public virtual Vector3 getPosition(Vector3 position) {
        return position;
    }
}
