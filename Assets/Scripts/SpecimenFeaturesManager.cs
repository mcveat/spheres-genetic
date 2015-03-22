using UnityEngine;
using System.Collections;

public class SpecimenFeaturesManager : MonoBehaviour {
	public SpecimenFeatures features;

	private SpecimenFeaturesRenderer featuresRenderer;
	private GenericMovement movement;

	public void Awake() {
		featuresRenderer = GetComponent<SpecimenFeaturesRenderer> ();
		movement = GetComponent<GenericMovement> ();
	}

	public void NewFeatures(SpecimenFeatures features) {
		this.features = features;
		transform.position = new Vector3 (transform.position.x, features.size / 2f, transform.position.z);
		transform.localScale = new Vector3 (features.size, features.size, features.size);
		featuresRenderer.NewFeatures (features);
		movement.SetSpeed (features.speed);
	}
}
