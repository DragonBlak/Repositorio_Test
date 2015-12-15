using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public int Health;
	public int currentHealth;
	public Slider healthBar;
	NavMeshAgent nav;
	GameObject Base;
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		currentHealth = Health;
		healthBar.value = currentHealth;
		Base = GameObject.FindGameObjectWithTag("Base");
		if (Base)
			nav.destination = Base.transform.position;
	}

	void Update(){
		if(Base == null)
			nav.enabled = false;
		healthBar.value = currentHealth;
		if(currentHealth <= 0){
			Destroy(this.gameObject);
		}
	}
}
