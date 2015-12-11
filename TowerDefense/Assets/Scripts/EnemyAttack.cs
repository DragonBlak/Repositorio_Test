using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	
	public int damage = 10;
	BuildingHealth bHealth;

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Base") {
			bHealth = collider.GetComponent<BuildingHealth>();
			bHealth.currentHealth -= damage;
		}
	}

}
