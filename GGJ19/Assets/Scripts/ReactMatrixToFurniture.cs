using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactMatrixToFurniture : MonoBehaviour {
	Renderer _rend;
	public float alphaValue;

	bool enterOnMatrix;
	private MeshRenderer mr;

	public float upSpeed;

	private void Start() {
		mr = GetComponent<MeshRenderer>();
	}

	//private void OnCollisionEnter(Collision collision) {
	//	if (collision.collider.CompareTag("matrix"))
	//	{
	//		enterOnMatrix = true;
	//		mr = collision.collider.GetComponent<MeshRenderer>();
	//		mr.enabled = true;

	//		collision.collider.gameObject.transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
	//	}
	//}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("furniture")) 
		{
			if (!enterOnMatrix) 
			{
				enterOnMatrix = true;
				mr.enabled = true;

				transform.Translate(Vector3.up* upSpeed * Time.deltaTime);
}
		}

	}

	void OnTriggerExit(Collider other) {
		if (enterOnMatrix)
		{
			enterOnMatrix = false;
			other.gameObject.transform.Translate(Vector3.down * upSpeed * Time.deltaTime);
		}
	}

	//_rend = hit.collider.GetComponent<Renderer>();s
	//Color oldColor = _rend.material.color;
	//Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);
	//_rend.material.SetColor("_Color", newColor);

}
