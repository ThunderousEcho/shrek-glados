using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public const float speed = 4;

	void Start () {
		
	}
	
	void Update () {
        Vector2 f = transform.up * speed * Time.deltaTime;

        RaycastHit2D h = Physics2D.Raycast(transform.position, f, speed * Time.deltaTime);
        if (h.collider != null) {
            h.collider.gameObject.SendMessage("damage", 1);
            Destroy(gameObject);
        }

        transform.position += (Vector3)f;
    }
}
