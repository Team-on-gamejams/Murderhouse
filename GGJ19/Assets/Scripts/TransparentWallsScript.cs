using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentWallsScript : MonoBehaviour
{
	[SerializeField] private float minAlpha;

	[SerializeField] private GameObject[] WallsArr;

	Renderer _rend;

	public void Activate()
	{
		foreach (GameObject i in WallsArr) //the line the error is pointing to
		{
			_rend = i.GetComponent<Renderer>();
			if (_rend != null) 
			{

				i.layer = 2;
				Color oldColor = _rend.material.color;
				Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, minAlpha);
				_rend.material.SetColor("_Color", newColor);
			}
			_rend = null;

		}
	}

	public void Deactivate()
	{
		foreach (GameObject i in WallsArr) //the line the error is pointing to
			{
			_rend = i.GetComponent<Renderer>();
			if (_rend != null) {
				i.layer = 0;
				Color oldColor = _rend.material.color;
				Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b,1f);
				_rend.material.SetColor("_Color", newColor);
			}
			_rend = null;

		}
	}
}
