﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Sprite[] sprites;
    public SpriteRenderer rend;

    bool engineRunning = true;
    public ParticleSystem engineExhaust;

    public float movementSpeed;

    bool bulletsRunning = true;

    float a = 0;

    Vector3 lastPos;

    float health = 1;

    float peekScale;
    public Transform peek;

    public LineRenderer laser;
    Vector3[] laserPoints = new Vector3[32];

    public static PlayerController main;

    public GameObject gameOverText;

    public void damage(float amount) {
        health -= amount;
        rend.color = Color.Lerp(Color.white, Color.red, (1 - health / 5));
        if (health <= 0) {
            GetComponent<Collider2D>().enabled = false;
            rend.enabled = false;
            gameOverText.SetActive(true);
            laser.enabled = false;
            peek.gameObject.SetActive(false);
        }
    }

    public void heal() {
        health = 5;
        rend.color = Color.Lerp(Color.white, Color.red, (1 - health / 5));
    }

    void Start() {
        main = this;
    }

    void Update() {

        if (health <= 0)
            return;

        if (World.transitionStartTime > -64) {
            transform.position = Vector3.Lerp(transform.position, new Vector3(-6, 0, transform.position.z), Time.deltaTime * 5);
        }

        int spriteNow = 0;

        Vector3 mov = Vector2.zero;
        if (World.transitionStartTime < -64) {
            mov.x = Input.GetAxisRaw("Horizontal");
            mov.y = Input.GetAxisRaw("Vertical");
            mov = Vector2.ClampMagnitude(mov, 1);
            transform.position += mov * movementSpeed;
        }

        if (mov.magnitude != 0) {
            spriteNow++;
            if (!engineRunning) {
                engineExhaust.Play();
                engineRunning = true;
            }
        } else {
            if (engineRunning) {
                engineExhaust.Stop();
                engineRunning = false;
            }
        }

        Plane p = new Plane(transform.forward, transform.position);
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter;
        p.Raycast(r, out enter);
        Vector2 mousePos = r.GetPoint(enter);
        Vector2 relMousePos = mousePos - (Vector2)transform.position;

        transform.LookAt(transform.position + Vector3.forward, relMousePos);

        if (Input.GetButton("Fire1")) {
            RaycastHit2D h = Physics2D.Raycast(transform.position + transform.up, transform.up);
            if (h.collider != null) {
                h.collider.gameObject.SendMessage("damage", Time.deltaTime);
            }

            spriteNow += 2;
            laserPoints[0] = transform.position + transform.up * 0.75f;
            for (int i = 1; i < laserPoints.Length; i++) {
                if (i + 0.75f > h.distance - 1 && h.collider != null) {
                    laserPoints[i] = transform.position + transform.up * (h.distance + 1) + (Vector3)Random.insideUnitCircle * 0.125f;
                    continue;
                }
                laserPoints[i] = (transform.position + transform.up * (i + 0.75f)) + (Vector3)Random.insideUnitCircle * 0.125f;
            }
            laser.SetPositions(laserPoints);
            laser.enabled = true;

            if (World.transitionStartTime > -64)
                peekScale = Mathf.Lerp(peekScale, 64, Time.deltaTime * 5);
            else
                peekScale = Mathf.Lerp(peekScale, 7, Time.deltaTime * 5);
        } else {
            laser.enabled = false;
            peekScale = Mathf.Lerp(peekScale, 64, Time.deltaTime * 5);
        }

        //rend.sprite = sprites[spriteNow];

        var er = engineExhaust.main;
        er.startRotation = Mathf.Atan2(mov.x, mov.y);

        //e.startRotation = Mathf.Atan2(relMousePos.x, relMousePos.y);

        /*Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);
        screenPos.x = Mathf.Clamp01(screenPos.x);
        screenPos.y = Mathf.Clamp01(screenPos.y);
        Vector3 n = Camera.main.ViewportToWorldPoint(screenPos);
        n.z = -9;
        transform.position = n;*/

        Vector3 x = transform.position;
        x.y = Mathf.Clamp(x.y, -5f, 5f);
        x.x = Mathf.Clamp(x.x, -5f * 16f / 9f, 5f * 16f / 9f);
        x.z = -9;
        transform.position = x;

        peek.localScale = peekScale * new Vector3(1, 1, 0) + Vector3.forward;
        peek.rotation = Quaternion.identity;
    }
}
