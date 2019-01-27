using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController : MonoBehaviour
{
	[SerializeField] private int money;
	
	public bool Minus(int _cost)
	{
		if (money >= _cost)
		{
			money -= _cost;
			return true;
		} else {
			Debug.Log("Enough money!");
			return false;
		}
	}

	public int Money()
	{
		return money;
	}
}
