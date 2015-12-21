using UnityEngine;
using System.Collections;

public class TeslaRay : MonoBehaviour {

	[HideInInspector]public int damage;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void RayDamage(Transform Target){
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Target.position - transform.position, out hit)) {
			switch (hit.transform.gameObject.tag) {
				case "Enemy":
					EnemyController enemyController = Target.GetComponent<EnemyController>();
					enemyController.currentHealth -= damage;
				break;
			}
		}
	}
}
