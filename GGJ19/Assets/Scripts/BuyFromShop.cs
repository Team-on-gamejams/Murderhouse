using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyFromShop : MonoBehaviour
{
	int cost;

	public GameObject gameObjectPrefab;
	public Move3D grabSystem;

	public Camera _camera;

	public float distance = 10f;

    public void BuyObject ()
	{
		Instantiate(gameObjectPrefab, new Vector3(1f,1f,1f), new Quaternion(0f, 0f, 0f, 0f));

		grabSystem.gm = this.gameObjectPrefab;
		grabSystem.pressed = true;
	}
}
