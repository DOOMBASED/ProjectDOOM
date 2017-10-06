using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class ItemName : MonoBehaviour
{

	public string itemName;

	void Start()
	{
		SetItemName();
	}

	void SetItemName()
	{
		transform.name = itemName;
	}
}
}