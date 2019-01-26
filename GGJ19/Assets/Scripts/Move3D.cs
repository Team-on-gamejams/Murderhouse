using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move3D : MonoBehaviour 
{

	public float distance;

	public Camera _camera;

	public GameObject gm;
	public bool pressed = false;

	Vector3 mousePos;

	void Start() {

	}

	void Update() {
		Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

		
		if (Physics.Raycast(ray, out hit)) {			
			if (Input.GetMouseButtonDown(0))
			{
				if (hit.collider.CompareTag("furniture")) {
					
					gm.transform.position = hit.point;
					pressed = !pressed;
				}
				
			}
			if (gm != null && !hit.collider.CompareTag("furniture") && pressed)
			{
				gm.transform.position = hit.point;
			}
		}

	}
	}
