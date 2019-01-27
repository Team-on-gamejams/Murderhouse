using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyFromShop : MonoBehaviour {
	public FurnitureController fr;
	public GameObject gameObjectPrefab;

	public Transform spawnPos;
	public int scale = 1;

	MoneyController money;

	void Start() {
		money = GameObject.FindGameObjectWithTag("MoneyController").GetComponent<MoneyController>();
	}

	public void BuyObject() {
		if (!fr.IsAll()) {
			GameObject go = Instantiate(gameObjectPrefab, spawnPos.position, spawnPos.rotation);
			go.transform.localScale = new Vector3(scale, scale, scale);
			FurnitureBonus fb = go.GetComponent<FurnitureBonus>();

			if (money.CanBuy(fb.price)) {
				fr.AddFurniture(go);
				money.Withdraw(fb.price);
			}
			else {
				//TODO: Вивести щось про нестачу грошей
				Destroy(go);
			}
		}
	}
}
