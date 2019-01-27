using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour {
	public int money = 25;

	public bool CanBuy(int price){
		return price <= money;
	}

	public void Withdraw(int price) {
		money -= price;
	}

	public void Deposit(int price) {
		money += price;
	}
}
