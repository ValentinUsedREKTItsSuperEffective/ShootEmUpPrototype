using System;
using UnityEngine;

public class ShipRotation : MonoBehaviour {
    
    public BaseEntity player;

    private readonly float rotationZLimit = 12f*Mathf.PI/360f;
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey (KeyCode.LeftArrow)) {
            if(transform.rotation.z > rotationZLimit) {
                return;
            }

            transform.Rotate(new Vector3(0, 0, player.speed));
        }

        if (Input.GetKey (KeyCode.RightArrow)) {
            if (transform.rotation.z < -rotationZLimit) {
                return;
            }

            transform.Rotate (new Vector3 (0, 0, -player.speed));
        }
	}
}
