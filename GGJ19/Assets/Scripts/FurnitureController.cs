using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureController : MonoBehaviour
{
	public int max;

	public List<GameObject> furnitureArr;

	public void AddFurniture(GameObject furniture)
	{
		if (furnitureArr.Count >= max)
			return;
		else {
			furnitureArr.Add(furniture);
		}
	}

	public bool IsAll()
	{
		if (furnitureArr.Count >= max)
		{
			return true;
		} else {
			return false;
		}
	}
}
