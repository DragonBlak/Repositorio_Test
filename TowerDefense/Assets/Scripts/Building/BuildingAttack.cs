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
	public bool isTesla = false;
	public LineRenderer teslaFR;
	public GameObject toInstantiate;
	float nextFire;
	bool canFire = false;
	SphereCollider sCollider;
	int targetAssign;

	void Start () {
		sCollider = GetComponent<SphereCollider> ();
		sCollider.radius = Range;
	}
	

	void Update () {
		if (target.Length == 0)
			targetAssign = -1;
		//Debug.DrawRay(transform.position, target[targetAssign].transform.position - transform.position , Color.green);
		if (isTesla) {
			Tesla();
		}
		if (canFire && isTesla == false && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Fire ();
		}
	}
	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Enemy") {
			for (int i = 0; i < target.Length; i++) {
				if(targetAssign == -1){
					targetAssign = i;
					canFire = true;
					target [targetAssign] = collider.gameObject;
					target [targetAssign].transform.position = new Vector3 (target [targetAssign].transform.position.x, transform.position.y, target [targetAssign].transform.position.z);
					transform.LookAt (target[targetAssign].transform);
				}
			}
		}
	}	
	void OnTriggerStay(Collider collider) {
		if (collider.gameObject.tag == "Enemy") {
			if(targetAssign != -1){
				canFire = true;
				target[targetAssign].transform.position = new Vector3(target[targetAssign].transform.position.x, transform.position.y, target[targetAssign].transform.position.z);
				transform.LookAt(target[targetAssign].transform);
			}
			else {
				AssignTargetOnDestoy();
				target [targetAssign] = collider.gameObject;
				target [targetAssign].transform.position = new Vector3 (target [targetAssign].transform.position.x, transform.position.y, target [targetAssign].transform.position.z);
				transform.LookAt (target [targetAssign].transform);
			}
		}
	}
	void OnTriggerExit(Collider collider){
		if (collider.gameObject.tag == "Enemy") {
			targetAssign = -1;
		}
	}
	

	public void Fire(){
		if (targetAssign == -1) 
			return;

		if (target[targetAssign] == null)
		{
			targetAssign = -1;
			return;
		}
		
		toInstantiate = Instantiate (projectile, firePos.position, firePos.rotation) as GameObject;
		Vector3 dir = target[targetAssign].transform.position - toInstantiate.transform.position;
		toInstantiate.gameObject.GetComponent<Rigidbody>().velocity = dir.normalized * bulletSpeed;
		toInstantiate.GetComponent<BulletDamage> ().damage = damage;
		Destroy (toInstantiate.gameObject, 2);
		canFire = false;
	}

	public void Tesla(){
		if (isTesla) {
			if (targetAssign == -1) 
				return;
			if (target[targetAssign] == null)
			{
				targetAssign = -1;
				return;
			}
			toInstantiate =	Instantiate (teslaFR.gameObject, transform.position, transform.rotation) as GameObject;
			TeslaRay rayScript = toInstantiate.GetComponent<TeslaRay>();
			rayScript.damage = damage; 
			teslaFR.SetPosition(0, transform.position);
			Vector3 A = transform.position;
			Vector3 B = target[targetAssign].transform.position;
			float X = Mathf.Lerp(0 , Vector3.Distance(transform.position,target[targetAssign].transform.position) , 1);
			Vector3 point = X * Vector3.Normalize(B - A) + A;
			
			teslaFR.SetPosition(1, point);

			rayScript.RayDamage(target[targetAssign].transform);
			Destroy(toInstantiate, 0.08f);
		}
	}
	public void AssignTargetOnDestoy(){
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		for(int i = 0; i < target.Length; i++){
			Vector3 diff = target[i].transform.position - position;
			float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) {
					targetAssign = i;
					distance = curDistance;
				}
		}
	}
}
