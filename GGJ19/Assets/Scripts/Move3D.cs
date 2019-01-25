using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move3D : MonoBehaviour 
{

	public float distance;

	public Camera _camera;

	private GameObject gm;
	public float speed = 1f;
	public bool pressed;

	void Start() {

	}

	void FixedUpdate() {
		Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

		
		if (Physics.Raycast(ray, out hit)) {			
			if (Input.GetMouseButtonDown(0))
			{
				if (hit.collider.CompareTag("furniture")) {
					gm = hit.collider.gameObject;
					pressed = !pressed;
					if (!pressed)
						gm = null;
				}
			}
		}
		
		if (gm != null)
		{
			Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
			Vector3 objPosition = _camera.ScreenToWorldPoint(mousePosition);
			gm.transform.position = objPosition * speed;
		}

	}
	}
