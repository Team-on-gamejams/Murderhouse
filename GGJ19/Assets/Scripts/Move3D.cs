using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move3D : MonoBehaviour {

	public float distance;
	public float moveSpeed;

	public Camera _camera;

	void Start() {

	}

	void Update() {
		Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance); //переменной записываються координаты мыши по иксу и игрику
		Vector3 objPosition = _camera.ScreenToWorldPoint(mousePosition); // переменной - объекту присваиваеться переменная с координатами мыши

		transform.position = objPosition * moveSpeed;


	}


	//void Update()
	//{

	//	//Vector3 p =	_camera.ScreenToWorldPoint(Input.mousePosition);

	//	//Vector3 wp = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.nearClipPlane));
	//	//transform.position = wp - transform.position;


	//	//float h = horizontalSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
	//	//transform.position = new Vector3(h, 0f, 0f);

	//	//Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance); // переменной записываються координаты мыши по иксу и игрику
	//	//Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition); // переменной - объекту присваиваеться переменная с координатами мыши
	//	//transform.position = objPosition; // и собственно объекту записываються координаты
	//}
}
