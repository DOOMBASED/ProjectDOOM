using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class PlayerAmmoBox : MonoBehaviour
{

	private PlayerMaster playerMaster;

	[System.Serializable]
	public class AmmoTypes
	{
		public string ammoName;
		public int ammoCurrentCarried;
		public int ammoMaxQuantity;
		public AmmoTypes(string aName, int aMaxQuantity, int aCurrentCarried)
		{
			ammoName = aName;
			ammoMaxQuantity = aMaxQuantity;
			ammoCurrentCarried = aCurrentCarried;
		}
	}

	public List<AmmoTypes> typesOfAmmunition = new List<AmmoTypes>();

	void OnEnable()
	{
		SetInitialReferences();
		playerMaster.EventPickedUpAmmo += PickedUpAmmo;
	}

	void OnDisable()
	{
		playerMaster.EventPickedUpAmmo -= PickedUpAmmo;
	}

	void Start()
	{
		SetInitialReferences();
	}

	void SetInitialReferences()
	{
		playerMaster = GetComponent<PlayerMaster>();
	}

	void PickedUpAmmo(string ammoName, int quantity)
	{
		for (int i = 0; i < typesOfAmmunition.Count; i++)
		{
			if (typesOfAmmunition[i].ammoName == ammoName)
			{
				typesOfAmmunition[i].ammoCurrentCarried += quantity;
				if (typesOfAmmunition[i].ammoCurrentCarried > typesOfAmmunition[i].ammoMaxQuantity)
				{
					typesOfAmmunition[i].ammoCurrentCarried = typesOfAmmunition[i].ammoMaxQuantity;
				}
				playerMaster.CallEventAmmoChanged();
				break;
			}
		}
	}
}
}