using UnityEngine;
using System.Collections;

public class BuildingController : MonoBehaviour {
	
	float distance;
	float distToGround;
	Color startcolor;
	[HideInInspector]public float lastPosX;
	[HideInInspector]public float lastPosY;
	[HideInInspector]public float lastPosZ;
	Renderer rend;
	GameObject gameManager;
	GameManager gameMscript;
	bool isGrounded(){
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}
	bool TowerDetected = false;

	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameController");
		gameMscript = gameManager.GetComponent<GameManager> ();
		rend = GetComponent<Renderer> ();
		startcolor = rend.material.GetColor("_Color");
		distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;

	}

	void Update () {
		if (gameMscript.enemiesInScene == 0) 
			RestartRotation ();
		if(Input.GetMouseButtonUp(0))
		{
			OnBeingPlaced();
		}
	}
	void OnMouseDrag(){
		if (gameMscript.enemiesInScene == 0) {
			MoveTower ();
		}
	}
	void MoveTower(){
		distance = Camera.main.transform.position.y;
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distance);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);
		transform.position = new Vector3 ((int)objPosition.x, 1f, (int)objPosition.z);
		if (isGrounded() && TowerDetected == false) {
			rend.material.color = Color.green;
		} 
		else
		{
			rend.material.color = Color.red;
		}
	}
	void OnBeingPlaced(){			
		if(isGrounded() && TowerDetected == false)
		{
			rend.material.color = startcolor;
			lastPosX = transform.position.x;
			lastPosY = transform.position.y;
			lastPosZ = transform.position.z;	
		}
		else
		{
			transform.position = new Vector3(lastPosX,lastPosY,lastPosZ);
			rend.material.color = startcolor;				
		}
	}
	void RestartRotation(){
		transform.rotation = Quaternion.Euler (0, 0, 0);
	}
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Tower") {
			TowerDetected = true;
		}
	}
	void OnCollisionExit(Collision other){
		if (other.gameObject.tag == "Tower") {
			TowerDetected = false;
		}
	}
}
