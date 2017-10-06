using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class ItemMaster : MonoBehaviour
{

	private PlayerMaster playerMaster;

	private bool isOnPlayer;

	public delegate void GeneralEventHandler();
	public delegate void PickupActionEventHandler(Transform item);
	public event GeneralEventHandler EventObjectThrow;
	public event GeneralEventHandler EventObjectPickup;
	public event PickupActionEventHandler EventPickupAction;

	void OnEnable()
	{
		SetInitialReferences();
	}

	void Start()
	{
		SetInitialReferences();
		CheckIfOnPlayer();
	}

	void SetInitialReferences()
	{
		if (GameManagerReferences._player != null)
		{
			playerMaster = GameManagerReferences._player.GetComponent<PlayerMaster>();
		}
	}

	public void CallEventObjectThrow()
	{
		if (EventObjectThrow != null)
		{
			EventObjectThrow();

		}
		if (isOnPlayer)
		{
			playerMaster.CallEventHandsEmpty();
			playerMaster.CallEventInventoryChanged();
			CheckIfOnPlayer();
		}

	}

	public void CallEventObjectPickup()
	{
		if (EventObjectPickup != null)
		{
			EventObjectPickup();

		}
		if (!isOnPlayer)
		{
			playerMaster.CallEventInventoryChanged();
			CheckIfOnPlayer();
		}

	}

	public void CallEventPickupAction(Transform item)
	{
		if (EventPickupAction != null)
		{
			EventPickupAction(item);
		}
	}

	void CheckIfOnPlayer()
	{
		if (transform.root == GameManagerReferences._player.transform)
		{
			isOnPlayer = true;
		}
		else
		{
			isOnPlayer = false;
		}
	}
}
}