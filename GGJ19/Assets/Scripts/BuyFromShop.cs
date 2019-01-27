using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyFromShop : MonoBehaviour {
	public FurnitureController fr;
	public GameObject gameObjectPrefab;

	public Transform spawnPos;
	public int scale = 1;

	public void BuyObject() {
		if (!fr.IsAll()) {
			Instantiate(gameObjectPrefab, spawnPos.position, spawnPos.rotation).transform.localScale = new Vector3(scale, scale, scale);
			fr.AddFurniture(gameObjectPrefab);
		}
	}
}
