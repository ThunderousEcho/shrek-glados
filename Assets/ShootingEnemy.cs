using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy {

    public GameObject bulletPrefab;
    float fireTimer;
    float lastFired;

    float timeBetweenShots = 1f;

    public override void Start() {
        base.Start();
        maxHealth = 3;
        health = 3;
    }

    public override void Update () {
        base.Update();

        if (World.transitionStartTime > -64)
            return;

        fireTimer += Time.deltaTime;

        if (fireTimer - lastFired > timeBetweenShots) {
            lastFired += timeBetweenShots;

            Transform t = Instantiate(bulletPrefab, transform.position, Quaternion.identity).transform;
            t.LookAt(t.position + Vector3.forward, PlayerController.main.transform.position - t.position);

            t.transform.position += t.up * (0.75f + (fireTimer - lastFired) * Bullet.speed);
        }
    }
}
