using UnityEngine;
using System.Collections;

public class SpecimenCollision : MonoBehaviour {
	private SpecimenMovement movement;
	private Genetics genetics;
	private float lastCross = -1000f;

	void Awake() {
		genetics = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Genetics> ();
		movement = gameObject.GetComponent<SpecimenMovement> ();
	}
	void OnTriggerEnter(Collider other) {
		if (other.tag != "Player" || !ReadyToCross())
			return;
		movement.PlayerCameClose ();
	}

	void OnTriggerStay(Collider other) {
		bool crossPossible = other.tag == "Player" && ReadyToCross () && Input.GetButton ("Jump");
		if (!crossPossible)
			return;
		SpecimenFeaturesManager thisManager = GetComponent<SpecimenFeaturesManager> ();
		SpecimenFeaturesManager otherManager = other.GetComponent<SpecimenFeaturesManager> ();
		Tuple<SpecimenFeatures, SpecimenFeatures> result = 
			genetics.Cross(thisManager.features, otherManager.features);
		thisManager.NewFeatures (result.First);
		otherManager.NewFeatures (result.Second);
		lastCross = Time.time;
		movement.PlayerLeft ();
		Debug.Log (
			"Cross finished:\n" +
			"in: " + thisManager.features.ToString() + "\n" +
			"in: " + otherManager.features.ToString() + "\n" +
			"out: " + result.First.ToString() + "\n" +
			"out: " + result.Second.ToString() + "\n"
		);
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player")
			movement.PlayerLeft ();
	}

	private bool ReadyToCross() {
		return Time.time > (lastCross + genetics.crossRate);
	}
}
