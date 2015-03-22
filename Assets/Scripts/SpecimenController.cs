using UnityEngine;
using System.Collections;

public class SpecimenController : MonoBehaviour {
	public Transform prefab;
	public int number = 20;
	public float maxX;
	public float maxZ;

	private GameObject specimens;
	private Genetics genetics;

	void Awake() {
		specimens = new GameObject ("Specimens");
		specimens.transform.SetParent (transform);
		genetics = GetComponent<Genetics> ();
	}

	void Start() {
		SetUpPlayer ();
		CreateRandomSpecimens ();
	}

	private void SetUpPlayer() {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		SpecimenFeaturesManager featuresManager = player.GetComponent<SpecimenFeaturesManager> ();
		featuresManager.NewFeatures (genetics.playerAtStart);
	}

	private void CreateRandomSpecimens() {
		for (int i = 0; i < number; ++i)
			CreateRandomSpecimen ();
	}

	private void CreateRandomSpecimen() {
		Vector3 position = new Vector3(Random.Range(-maxX, maxX), 0.5f, Random.Range (-maxZ, maxZ));
		Transform specimen = Instantiate(prefab, position, Quaternion.identity) as Transform;
		specimen.SetParent(specimens.transform);
		SpecimenFeaturesManager renderer = specimen.GetComponent<SpecimenFeaturesManager>();
		renderer.NewFeatures(genetics.Randomized());
	}
}
