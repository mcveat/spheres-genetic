using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpecimenFeatures {
	public Color color;
	public float metallic;
	public float smoothness;
	public float speed;
	public float size;

	public SpecimenFeatures(Color color, float metallic, float smoothness, float speed, float size) {
		this.color = color;
		this.metallic = metallic;
		this.smoothness = smoothness;
		this.speed = speed;
		this.size = size;
	}

	override public string ToString() {
		return "[color='" + color.ToString () + "',metallic='" + metallic + "',smoothness='" + smoothness + "',speed='" + speed + "',size='" + size + "]";
	}
}
