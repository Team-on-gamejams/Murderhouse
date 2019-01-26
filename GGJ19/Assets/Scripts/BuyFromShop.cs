using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyFromShop : MonoBehaviour
{
	public FurnitureController fr;
	public GameObject gameObjectPrefab;

    public void BuyObject ()
	{
		if (!fr.IsAll()) {
			Instantiate(gameObjectPrefab, new Vector3(1f, 2f, 1f), new Quaternion(0f, 0f, 0f, 0f));
			fr.AddFurniture(gameObjectPrefab);
		}
	}
}
