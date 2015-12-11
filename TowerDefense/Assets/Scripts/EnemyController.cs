using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public int Health;
	public int currentHealth;
	void Start () {
		currentHealth = Health;
		GameObject Base = GameObject.FindGameObjectWithTag("Base");
		if (Base)
			GetComponent<NavMeshAgent>().destination = Base.transform.position;
	}

	void Update(){
		if(currentHealth <= 0){
			Destroy(this.gameObject);
		}
	}
}
