using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class ItemUI : MonoBehaviour
{

	private ItemMaster itemMaster;

	public GameObject myUI;

	void OnEnable()
	{
		SetInitialReferences();
		itemMaster.EventObjectPickup += EnableMyUI;
		itemMaster.EventObjectThrow += DisableMyUI;
	}

	void OnDisable()
	{
		itemMaster.EventObjectPickup -= EnableMyUI;
		itemMaster.EventObjectThrow -= DisableMyUI;
	}

	void Start()
	{
		SetInitialReferences();
	}

	void SetInitialReferences()
	{
		itemMaster = GetComponent<ItemMaster>();
	}

	void EnableMyUI()
	{
		if (myUI != null)
		{
			myUI.SetActive(true);
		}
	}

	void DisableMyUI()
	{
		if (myUI != null)
		{
			myUI.SetActive(false);
		}
	}
}
}