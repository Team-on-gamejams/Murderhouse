using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureBonus : MonoBehaviour {
	public sbyte[] statsBonus = new sbyte[(int)StatsHolder.Stat.LAST_STAT];
	public int price;
}
