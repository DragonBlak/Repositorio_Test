using UnityEngine;
using System.Collections;

public class BuildingController : MonoBehaviour {

	//BUG: 1- To be fix: Not allow to place a tower inside another tower's spot
	float distance;
	float distToGround;
	Color startcolor;
	[HideInInspector]public float lastPosX;
	[HideInInspector]public float lastPosY;
	[HideInInspector]public float lastPosZ;
	Collider[] colliders;
	Renderer rend;
	GameObject gameManager;
	GameManager gameMscript;
	bool isGrounded(){
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
	}

	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameController");
		gameMscript = gameManager.GetComponent<GameManager> ();
		rend = GetComponent<Renderer> ();
		startcolor = rend.material.GetColor("_Color");
		colliders = GetComponents<Collider> ();
		distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;

	}

	void Update () {
		if(Input.GetMouseButtonUp(0))
		{
			if(isGrounded())
			{
				rend.material.color = startcolor;
				lastPosX = transform.position.x;
				lastPosY = transform.position.y;
				lastPosZ = transform.position.z;
				for (int i = 0; i < colliders.Length; i++) {
					colliders[i].enabled = true;
				}

			}
			else
			{
				Debug.Log ("You can't place an object here!");
				transform.position = new Vector3(lastPosX,lastPosY,lastPosZ);
				rend.material.color = startcolor;				
				for (int i = 0; i < colliders.Length; i++) {
					colliders[i].enabled = true;
				}

			}
		}
	}
	void OnMouseDrag(){
		MoveTower ();
	}
	void MoveTower(){
		for (int i = 0; i < colliders.Length; i++) {
			colliders[i].enabled = false;
		}
		distance = Camera.main.transform.position.y;
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distance);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);
		transform.position = new Vector3 ((int)objPosition.x, 1f, (int)objPosition.z);
		if (isGrounded()) {
			rend.material.color = Color.green;
		} 
		else
		{
			rend.material.color = Color.red;
		}
	}
}
