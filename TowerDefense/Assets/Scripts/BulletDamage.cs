using UnityEngine;
using System.Collections;

public class BulletDamage : MonoBehaviour {

	EnemyController enemy;
	[HideInInspector]public int damage;
	void Update(){
		Destroy (this.gameObject, 3);
	}
	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Enemy") {
			enemy = collider.GetComponent<EnemyController>();
			enemy.currentHealth -= damage;
			Destroy(this.gameObject);
		}
	}
}
