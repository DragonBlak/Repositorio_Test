using UnityEngine;
using System.Collections;

public class BuildingAttack : MonoBehaviour {
	
	public int bulletSpeed;
	public int damage;
	public GameObject[] target;
	public Transform firePos;
	public GameObject projectile;
	public float fireRate;
	public float Range = 4;
	float[] distBP;
	GameObject toInstantiate;
	float nextFire;
	bool canFire = false;
	SphereCollider sCollider;
	int targetAssign;

	void Start () {
		sCollider = GetComponent<SphereCollider> ();
		sCollider.radius = Range;
	}
	

	void Update () {
		if (canFire && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Fire ();
		}
		if (target.Length == 0)
			targetAssign = -1;
	}
	public void Fire(){
		if (target == null)
			targetAssign = -1;
		if (target [targetAssign].gameObject == null)
			targetAssign = -1;
		if (targetAssign == -1) 
			return;

		toInstantiate = Instantiate (projectile, firePos.position, firePos.rotation) as GameObject;
		Vector3 dir = target[targetAssign].transform.position - toInstantiate.transform.position;
		toInstantiate.gameObject.GetComponent<Rigidbody>().velocity = dir.normalized * bulletSpeed;
		toInstantiate.GetComponent<BulletDamage> ().damage = damage;
		Destroy (toInstantiate.gameObject, 2);
		canFire = false;
	}
	void OnTriggerStay(Collider collider) {
		if (collider.gameObject.tag == "Enemy") {
			for (int i = 0; i < target.Length; i++) {
				distBP = new float[target.Length];
				float minDist = Mathf.Infinity;
				distBP[i] = Vector3.Distance(transform.position, target[i].transform.position);
				if(distBP[i] < minDist)
				{
					targetAssign = i;
				}
			}
			canFire = true;
			target[targetAssign] = collider.gameObject;
			target[targetAssign].transform.position = new Vector3(target[targetAssign].transform.position.x, transform.position.y, target[targetAssign].transform.position.z);
			transform.LookAt(target[targetAssign].transform);
		}

	}	
	void OnTriggerExit(Collider collider) {
		if (collider.gameObject.tag == "Enemy") {
			targetAssign = -1;
		}
	}

}
