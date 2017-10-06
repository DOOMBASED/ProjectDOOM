using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class ItemTag : MonoBehaviour
{

	public string itemTag;

	void OnEnable()
	{
		SetTag();
	}

	void SetTag()
	{
		if (itemTag == "")
		{
			itemTag = "Untagged";
		}
		transform.tag = itemTag;
	}
}
}