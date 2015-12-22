using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class ShopList{
	public int id;
	public GameObject towerPrefab;
	public Sprite towerSprite;
	public string name;
	public int price;
}

public class ShopSystem : MonoBehaviour {

	public GameObject shopPanel;
	public GameObject shopContent;
	public GameObject slot;

	public List<ShopList> shopList;
	bool shopEnable(){
		if (shopPanel.activeInHierarchy == true) {
			return true;
		} else
			return false;
	}

	void Start () {
		shopPanel.SetActive(false);
		addItems ();
	}

	void Update () {
		if (Input.GetKeyUp (KeyCode.Y)) {
			DisplayShop();
		}
	}

	void addItems(){
		foreach (var item in shopList) {
			GameObject newSlot = Instantiate (slot) as GameObject;
			newSlot.transform.SetParent (shopContent.transform);
			newSlot.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
			ShopButton properties = newSlot.GetComponent<ShopButton> ();

			properties.thisId = item.id;
			properties.thisGameObject = item.towerPrefab;
			properties.thisName = item.name;
			properties.thisPrice = item.price;
			properties.thisSprite = item.towerSprite;

			properties.priceText.text = item.price.ToString();
			properties.nameText.text = item.name;
			properties.towerImage.sprite = item.towerSprite;
		}
	}
	void DisplayShop(){
		if(shopEnable() == false){
			shopPanel.SetActive(true);
		}
		else 
			shopPanel.SetActive(false);
	}
	public void CloseShop(){
		shopPanel.SetActive(false);
	}
	
}
