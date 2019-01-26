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
		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance); // переменной записываються координаты мыши по иксу и игрику
		Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition); // переменной - объекту присваиваеться переменная с координатами мыши

		Instantiate(gameObjectPrefab, objPosition, new Quaternion(0f, 0f, 0f, 0f));

		grabSystem.gm = this.gameObjectPrefab;
		grabSystem.pressed = true;
	}
}
