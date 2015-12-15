using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildingHealth : MonoBehaviour {

	public int Health;
	public Slider healthBar;
	[HideInInspector]public int currentHealth;
	void Start () {
		currentHealth = Health;
		healthBar.value = currentHealth;
	}

	void Update () {
		healthBar.value = currentHealth;
		if (currentHealth <= 0) {
			Destroy(gameObject);
		}
	}
}
