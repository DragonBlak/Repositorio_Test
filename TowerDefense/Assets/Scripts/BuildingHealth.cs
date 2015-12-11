using UnityEngine;
using System.Collections;

public class BuildingHealth : MonoBehaviour {

	public int Health;
	[HideInInspector]public int currentHealth;
	void Start () {
		currentHealth = Health;
	}

	void Update () {
		if (currentHealth <= 0) {
			Destroy(gameObject);
		}
	}
}
