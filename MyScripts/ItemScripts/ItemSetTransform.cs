using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class ItemSetTransform : MonoBehaviour
{

	private ItemMaster itemMaster;

	public Vector3 itemLocalPosition;
	public Vector3 itemLocalRotation;

	void OnEnable()
	{
		SetInitialReferences();
		itemMaster.EventObjectPickup += SetPositionOnPlayer;
		itemMaster.EventObjectPickup += SetRotationOnPlayer;
	}

	void OnDisable()
	{
		itemMaster.EventObjectPickup -= SetPositionOnPlayer;
		itemMaster.EventObjectPickup -= SetRotationOnPlayer;
	}

	void Start()
	{
		SetInitialReferences();
		SetPositionOnPlayer();
		SetRotationOnPlayer();
	}

	void SetInitialReferences()
	{
		itemMaster = GetComponent<ItemMaster>();
	}

	void SetPositionOnPlayer()
	{
		if (transform.root.CompareTag(GameManagerReferences._playerTag))
		{
			transform.localPosition = itemLocalPosition;
		}
	}

	void SetRotationOnPlayer()
	{
		if (transform.root.CompareTag(GameManagerReferences._playerTag))
		{
			transform.localRotation = Quaternion.Euler(itemLocalRotation.x, itemLocalRotation.y, itemLocalRotation.z);
		}
	}
}
}