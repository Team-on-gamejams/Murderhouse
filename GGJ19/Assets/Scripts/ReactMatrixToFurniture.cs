using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactMatrixToFurniture : MonoBehaviour {
	Renderer _rend; // Render component this objects
	public float alphaValue; // the alpha value

	bool enterOnMatrix; // indicator for trigger

	public Move3D dragScript; // Drag script need for define pressed or no

	public float upSpeed; // Speed with which the this block move up or down 

	private void Start() {
		_rend = GetComponent<Renderer>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("furniture"))  // If other objects furniture
		{
			if (!enterOnMatrix) 
			{
				enterOnMatrix = true;

				alphaValue = 1f; // Change value what make block un-transparent

				Color oldColor = _rend.material.color;
				Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);
				_rend.material.SetColor("_Color", newColor);

				transform.Translate(Vector3.up* upSpeed * Time.deltaTime);
			}
		}

	}

	private void OnTriggerStay(Collider other) {
		if (!dragScript.pressed) { // If pressed == false
			other.transform.position = transform.position; // Set furniture on our position
		}
	}

	void OnTriggerExit(Collider other) {
		if (enterOnMatrix)
		{
			enterOnMatrix = false;
			other.gameObject.transform.Translate(Vector3.down * upSpeed * Time.deltaTime);

			alphaValue = 0f;

			Color oldColor = _rend.material.color;
			Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);
			_rend.material.SetColor("_Color", newColor);
		}
	}
}
