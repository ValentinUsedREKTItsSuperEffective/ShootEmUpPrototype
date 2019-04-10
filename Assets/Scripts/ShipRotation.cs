﻿using UnityEngine;

public class ShipRotation : MonoBehaviour {

    public EntityModel player;
    public Transform planet;

    private readonly float rotationZLimit = 12f*Mathf.PI/360f;
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey (KeyCode.LeftArrow)) {
            if(transform.rotation.z > rotationZLimit) {
                return;
            }

            transform.RotateAround (planet.position, new Vector3 (0, 0, 1), player.speed);
        }

        if (Input.GetKey (KeyCode.RightArrow)) {
            if (transform.rotation.z < -rotationZLimit) {
                return;
            }

            transform.RotateAround (planet.position, new Vector3 (0, 0, 1), -player.speed);
        }
	}
}
