using UnityEngine;
using System.Collections;

public class SpecimenMovement : GenericMovement {
	public float moveTime = 3f;
	public float idleTime = 5f;
	public float moveProbability = 0.4f;

	private float lastDecision = 0f;
	private bool moving = false;
	private Vector3 force = Vector3.forward;
	private bool playerNear = false;

	private Rigidbody body;

	void Awake() {
		body = GetComponent<Rigidbody> ();
	}

	void Start() {
		Decide ();
	}

	void Update() {
		if (playerNear)
			return;
		if (moving)
			Moving ();
		else
			Idling ();
	}

	void Moving() {
		if (Time.time > lastDecision + moveTime) {
			Decide ();
			return;
		}
		body.AddForce (force);
	}

	void Idling() {
		if (Time.time > lastDecision + idleTime)
			Decide ();
	}

	void Decide() {
		lastDecision = Time.time;
		moving = Random.value < moveProbability;
		if (moving) {
			Vector3 randomForce = new Vector3 (Random.Range (-1.0f, 1.0f), 0.0f, Random.Range (-1.0f, 1.0f));
			Vector3 center = Vector3.MoveTowards(body.position, Vector3.zero, 0.0f).normalized;
			Vector3 normalized = Vector3.Lerp(randomForce, center, 0.1f).normalized;
			normalized.Scale(new Vector3(speed, 0.0f, speed));
			force = normalized;
		}
	}

	public void PlayerCameClose() {
		playerNear = true;
	}

	public void PlayerLeft() {
		playerNear = false;
	}
}
