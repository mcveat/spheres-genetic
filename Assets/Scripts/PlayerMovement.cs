using UnityEngine;
using System.Collections;

public class PlayerMovement : GenericMovement {
	private Rigidbody body;

	void Awake() {
		body = GetComponent<Rigidbody> ();
	}

	void FixedUpdate() {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		body.AddForce(new Vector3(h * speed, 0.0f, v * speed));
	}
}
