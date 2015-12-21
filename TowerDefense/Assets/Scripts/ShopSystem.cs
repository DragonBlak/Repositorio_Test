using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour {

	public GameObject ShopPanel;
	bool shopEnable(){
		if (ShopPanel.activeInHierarchy == true) {
			return true;
		} else
			return false;
	}
	void Start () {
		ShopPanel.SetActive(false);
	}

	void Update () {
		if (Input.GetKeyUp (KeyCode.Y)) {
			DisplayShop();
		}
	}
	void DisplayShop(){
		if(shopEnable() == false){
			ShopPanel.SetActive(true);
		}
		else 
			ShopPanel.SetActive(false);
	}
}
