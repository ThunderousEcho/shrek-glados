using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;

	void Start () {
		
	}
	
	void Update () {
        Vector2 f = transform.up * speed * Time.deltaTime;

        RaycastHit2D h = Physics2D.Raycast(transform.position, f);
        if (h.collider != null) {
            h.collider.gameObject.SendMessage("damage", 1);
            Destroy(gameObject);
        }

        transform.position += transform.up * speed * Time.deltaTime;
    }
}
