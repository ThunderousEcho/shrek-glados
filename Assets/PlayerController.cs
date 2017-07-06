using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Sprite[] sprites;
    public SpriteRenderer rend;

    bool engineRunning = true;
    public ParticleSystem engineExhaust;

    public float movementSpeed;

    bool bulletsRunning = true;
    public ParticleSystem bullets;

    float a = 0;

    Vector3 lastPos;

    void Start() {

    }

    void Update() {
        int spriteNow = 0;

        Vector3 mov = Vector2.zero;
        mov.x = Input.GetAxisRaw("Horizontal");
        mov.y = Input.GetAxisRaw("Vertical");
        mov = Vector2.ClampMagnitude(mov, 1);

        transform.position += mov * movementSpeed;

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

        if (Input.GetButton("Fire1")) {
            spriteNow += 2;
            if (!bulletsRunning) {
                bullets.Play();
                bulletsRunning = true;
            }
        } else {
            if (bulletsRunning) {
                bullets.Stop();
                bulletsRunning = false;
            }
        }

        rend.sprite = sprites[spriteNow];


        Plane p = new Plane(transform.forward, transform.position);
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter;
        p.Raycast(r, out enter);
        Vector2 mousePos = r.GetPoint(enter);
        Vector2 relMousePos = mousePos - (Vector2)transform.position;

        transform.LookAt(transform.position + Vector3.forward, relMousePos);

        var e = engineExhaust.main;
        e.startRotation = Mathf.Atan2(mov.x, mov.y);

        //e.startRotation = Mathf.Atan2(relMousePos.x, relMousePos.y);

        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);
        screenPos.x = Mathf.Clamp01(screenPos.x);
        screenPos.y = Mathf.Clamp01(screenPos.y);

        Vector3 n = Camera.main.ViewportToWorldPoint(screenPos);
        n.z = 0;
        transform.position = n;
    }
}
