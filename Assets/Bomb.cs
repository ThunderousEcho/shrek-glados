using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
        Collider2D h = Physics2D.OverlapPoint(transform.position);
        if (h != null && h.gameObject == PlayerController.main.gameObject) {
            PlayerController.main.heal();
            Destroy(gameObject);
        }
	}
}
