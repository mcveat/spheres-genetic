using UnityEngine;
using System.Collections;

public class SpecimenFeaturesRenderer : MonoBehaviour {
	private Renderer thisRenderer;

	void Awake() {
		thisRenderer = GetComponent<Renderer> ();
		if (tag != "Player")
			return;
		GameObject controller = GameObject.FindGameObjectWithTag ("GameController");
		SpecimenFeatures features = controller.GetComponent<Genetics> ().playerAtStart;
		NewFeatures (features);
	}

	public void NewFeatures(SpecimenFeatures features) {
		Material material = thisRenderer.material;
		material.color = features.color;
		material.SetFloat ("_Metallic", features.metallic);
		material.SetFloat ("_Smoothness", features.smoothness);
	}
}
