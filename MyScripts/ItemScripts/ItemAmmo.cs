using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class ItemAmmo : MonoBehaviour
{

	private ItemMaster itemMaster;
	private GameObject playerGO;

	public bool isTriggerPickup;
	public string ammoName;
	public int quantity;


	void OnEnable()
	{
		SetInitialReferences();
		itemMaster.EventObjectPickup += TakeAmmo;
	}

	void OnDisable()
	{
		itemMaster.EventObjectPickup -= TakeAmmo;
	}

	void Start()
	{
		SetInitialReferences();
	}

	void SetInitialReferences()
	{
		itemMaster = GetComponent<ItemMaster>();
		playerGO = GameManagerReferences._player;
		if (isTriggerPickup)
		{
			if (GetComponent<Collider>() != null)
			{
				GetComponent<Collider>().isTrigger = true;
			}
			if (GetComponent<Rigidbody>() != null)
			{
				GetComponent<Rigidbody>().isKinematic = true;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(GameManagerReferences._playerTag) && isTriggerPickup)
		{
			TakeAmmo();
		}
	}

	void TakeAmmo()
	{
		playerGO.GetComponent<PlayerMaster>().CallEventPickedUpAmmo(ammoName, quantity);
		Destroy(gameObject);
	}
}
}