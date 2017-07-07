using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclingEnemy : ShootingEnemy {

    public float startAngle;
    float startTime = -1;

    public override void Start() {
        base.Start();
        maxHealth = 3;
        health = 3;
    }

    public override Vector3 getPosition(Vector3 position) {
        if (startTime < -0.5f)
            startTime = Time.time + startAngle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(Time.time- startTime) * 5, Mathf.Sin(Time.time- startTime) * 5, position.z);
    }
}
