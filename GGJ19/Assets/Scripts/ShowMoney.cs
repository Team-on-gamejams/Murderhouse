using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMoney : MonoBehaviour
{
	public Text myText;
	public MoneyController _money;

	private void Update() {
		myText.text = _money.money.ToString();
	}
}
