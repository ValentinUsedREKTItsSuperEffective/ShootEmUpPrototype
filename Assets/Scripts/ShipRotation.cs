using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotation : MonoBehaviour {

    public BaseEntity player;
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey (KeyCode.LeftArrow)) {
            transform.Rotate(new Vector3(0, 0, player.speed));
        }

        if (Input.GetKey (KeyCode.RightArrow)) {
            transform.Rotate (new Vector3 (0, 0, -player.speed));
        }
	}
}
