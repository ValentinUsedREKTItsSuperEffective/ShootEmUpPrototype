﻿using UnityEngine;

public class Asteroid : Enemy {

    [HideInInspector] public Vector3 direction;

    void Awake() {
        direction = new Vector3 ();
    }

    void Update () {
        transform.position = new Vector3 (
            transform.position.x + direction.x*model.speed*Time.deltaTime, 
            transform.position.y + direction.y * model.speed * Time.deltaTime, 
            transform.position.z
        );
	}
}
