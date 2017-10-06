using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class ItemSetLayer : MonoBehaviour
{

	private ItemMaster itemMaster;

	public string itemThrowLayer;
	public string itemPickupLayer;

	void OnEnable()
	{
		SetInitialReferences();
		itemMaster.EventObjectPickup += SetItemToPickupLayer;
		itemMaster.EventObjectThrow += SetItemToThrowLayer;
	}

	void OnDisable()
	{
		itemMaster.EventObjectPickup -= SetItemToPickupLayer;
		itemMaster.EventObjectThrow -= SetItemToThrowLayer;
	}

	void Start()
	{
		SetInitialReferences();
		SetLayerOnEnable();
	}

	void SetInitialReferences()
	{
		itemMaster = GetComponent<ItemMaster>();
	}

	void SetItemToThrowLayer()
	{
		SetLayer(transform, itemThrowLayer);
	}

	void SetItemToPickupLayer()
	{
		SetLayer(transform, itemPickupLayer);
	}

	void SetLayerOnEnable()
	{
		if (itemPickupLayer == "")
		{
			itemPickupLayer = "Item";
		}
		if (itemThrowLayer == "")
		{
			itemThrowLayer = "Item";
		}
		if (transform.root.CompareTag(GameManagerReferences._playerTag))
		{
			SetItemToPickupLayer();
		}
		else
		{
			SetItemToThrowLayer();
		}
	}

	void SetLayer(Transform trans, string itemLayerName)
	{
		trans.gameObject.layer = LayerMask.NameToLayer(itemLayerName);
		foreach (Transform child in trans)
		{
			SetLayer(child, itemLayerName);
		}
	}
}
}