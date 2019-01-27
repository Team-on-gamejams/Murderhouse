using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureController : MonoBehaviour {
	public int max;
	public List<GameObject> furnitureArr;
	public sbyte[] statsBonus = new sbyte[(int)StatsHolder.Stat.LAST_STAT];
	public MoneyController money;

	public void Start() {
		for (int i = 0; i < statsBonus.Length; ++i)
			statsBonus[i] = 0;
	}

	public void AddFurniture(GameObject furniture) {

		if (furnitureArr.Count >= max)
			return;
		else {
			furnitureArr.Add(furniture);
			AddStatBonus(furniture.GetComponent<FurnitureBonus>());
		}
	}

	public bool IsAll() {
		if (furnitureArr.Count >= max)
			return true;
		else
			return false;
	}

	public void AddStatBonus(FurnitureBonus bonus) {
		for (int i = 0; i < statsBonus.Length; ++i)
			statsBonus[i] += bonus.statsBonus[i];
	}

	public void RemoveStatBonus(FurnitureBonus bonus) {
		for (int i = 0; i < statsBonus.Length; ++i)
			statsBonus[i] -= bonus.statsBonus[i];
	}
}
