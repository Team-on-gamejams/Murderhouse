using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move3D : MonoBehaviour 
{
	private Camera _camera; // Main camera

	public GameObject gm; // Our grab GameObject
	public bool pressed = false; // Left press mouse click indicator

	void Start() {
		_camera = GetComponent<Camera>(); // Get the component Camera from objects
	}

	void Update() {
		Ray ray = _camera.ScreenPointToRay(Input.mousePosition); // Drow ray to cursor
		RaycastHit hit; // hit variable
		Debug.DrawRay(ray.origin, ray.direction * 20, Color.yellow); // draw ray


		
		if (Physics.Raycast(ray, out hit)) {			
			if (Input.GetMouseButton(0)) // if we press left mouse button
			{
				pressed = true; 
				if (hit.collider.CompareTag("furniture")) { // if hit objects == furniture

					gm = hit.collider.gameObject; // save grab objects
					gm.layer = 2; // make grab objects non - raycast
				}
			} else {
				if (gm != null)
				{
					pressed = false;
					gm.layer = 0; // make grab objects raycast AGAIN!
					gm = null; // clear gm
				}
			} 
			if (gm != null && !hit.collider.CompareTag("furniture") && pressed)
			{
				gm.transform.position = hit.point; // move grab objects
			}
		}
		
	}
	}
