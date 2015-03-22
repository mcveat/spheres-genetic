using UnityEngine;
using System.Collections;

[System.Serializable]
public class Range {
	public static Range Default = new Range (0f, 1f);

	public float from;
	public float to;

	public Range(float from, float to) {
		this.from = from;
		this.to = to;
	}
}

public class Genetics : MonoBehaviour {
	// player start state
	public SpecimenFeatures playerAtStart = new SpecimenFeatures (Color.white, 0.5f, 0.5f, 120f, 1f);
	// how often specimen is capable of crossing
	public float crossRate = 10f;
	// speed range
	public Range speedRange = new Range(50f, 400f);
	// size range
	public Range sizeRange = new Range(0.5f, 3f);
	// minor mutation probability
	public float minorMutationProbability = 0.01f;
	// minor mutation impact
	public float minorMutationImpact = 0.2f;
	// random mutation probability
	public float randomMutationProbability = 0.001f;

	public SpecimenFeatures Randomized() {
		Color color = new Color(Random.value, Random.value, Random.value, Random.value);
		float speed = RandomInRange (speedRange);
		float size = RandomInRange (sizeRange);
		return new SpecimenFeatures(color, Random.value, Random.value, speed, size);
	}
	
	public Tuple<SpecimenFeatures, SpecimenFeatures> Cross(SpecimenFeatures first, SpecimenFeatures second) {
		SpecimenFeatures newFirst = CrossOnce (first, second);
		SpecimenFeatures newSecond = CrossOnce (second, first);
		return new Tuple<SpecimenFeatures, SpecimenFeatures>(newFirst, newSecond);
	}
	
	public SpecimenFeatures CrossOnce(SpecimenFeatures first, SpecimenFeatures second) {
		return new SpecimenFeatures(
			Cross (first.color, second.color),
			Cross (first.metallic, second.metallic),
			Cross (first.smoothness, second.smoothness),
			Cross (first.speed, second.speed, speedRange),
			Cross (first.size, second.size, sizeRange)
			);
	}
	
	public Color Cross(Color first, Color second) {
		return new Color (
			Cross (first.r, second.r),
			Cross (first.g, second.g),
			Cross (first.b, second.b),
			Cross (first.a, second.a)
			);
	}
	
	public float Cross(float first, float second) {
		return Cross (first, second, Range.Default);
	}
	
	public float Cross(float first, float second, Range range) {
		float split = Random.value;
		float recombined = first * split + second * (1f - split);
		float mutationRoll = Random.value;
		if (mutationRoll < 0.1) {
			float factor = Random.Range(-0.2f, 2f) * (range.to - range.from);
			return Mathf.Clamp(recombined + factor, range.from, range.to);
		}
		if (mutationRoll > 0.99)
			return Random.Range (range.from, range.to);
		return recombined;
	}
	
	public float RandomInRange(Range r) {
		return Random.Range(r.from, r.to);
	}
}
