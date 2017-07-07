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

    int health = 5;

    public void damage(int amount) {
        health -= amount;
        if (health < 0)
            Destroy(gameObject);
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

        //rend.sprite = sprites[spriteNow];

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
        //screenPos.y = Mathf.Clamp01(screenPos.y);
        Vector3 n = Camera.main.ViewportToWorldPoint(screenPos);
        n.z = 0;
        transform.position = n;

        Vector3 x = transform.position;
        x.y = Mathf.Clamp(x.y, -5f, 5f);
        //x.x = Mathf.Clamp(x.x, -5f * 16f / 9f, 5f * 16f / 9f);
        x.z = 0;
        transform.position = x;

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[bullets.particleCount];
        bullets.GetParticles(particles);

        for (int i = particles.Length - 1; i >= 0; i--) {
            Collider2D c = Physics2D.OverlapPoint(particles[i].position);
            if (c != null) {
                particles[i].remainingLifetime = 0; //kill
                c.gameObject.SendMessage("damage", 1);
            }
        }

        bullets.SetParticles(particles, particles.Length);
    }
}
