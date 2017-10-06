using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GunAmmo : MonoBehaviour
{

	private PlayerMaster playerMaster;
	private GunMaster gunMaster;
	private PlayerAmmoBox ammoBox;
	private Animator myAnimator;

	public string ammoName;
	public int clipSize;
	public int currentAmmo;
	public float reloadTime;

	void OnEnable()
	{
		SetInitialReferences();
		StartingSanityCheck();
		CheckAmmoStatus();
		gunMaster.EventPlayerInput += DecreaseAmmo;
		gunMaster.EventPlayerInput += CheckAmmoStatus;
		gunMaster.EventRequestReload += TryToReload;
		gunMaster.EventGunNotUsable += TryToReload;
		gunMaster.EventRequestGunReset += ResetGunReloading;
		if (playerMaster != null)
		{
			playerMaster.EventAmmoChanged += UIAmmoUpdateRequest;
		}
		if (ammoBox != null)
		{
			StartCoroutine(UpdateAmmoUIWhenEnabling());
		}
		if (gunMaster.isReloading)
		{
			ResetGunReloading();
		}
	}

	void OnDisable()
	{
		gunMaster.EventPlayerInput -= DecreaseAmmo;
		gunMaster.EventPlayerInput -= CheckAmmoStatus;
		gunMaster.EventRequestReload -= TryToReload;
		gunMaster.EventGunNotUsable -= TryToReload;
		gunMaster.EventRequestGunReset -= ResetGunReloading;
		if (playerMaster != null)
		{
			playerMaster.EventAmmoChanged -= UIAmmoUpdateRequest;
		}
	}

	void Start()
	{
		SetInitialReferences();
		StartCoroutine(UpdateAmmoUIWhenEnabling());
		if (playerMaster != null)
		{
			playerMaster.EventAmmoChanged += UIAmmoUpdateRequest;
		}
	}

	void SetInitialReferences()
	{
		gunMaster = GetComponent<GunMaster>();
		if (GetComponent<Animator>() != null)
		{
			myAnimator = GetComponent<Animator>();
		}
		if (GameManagerReferences._player != null)
		{
			playerMaster = GameManagerReferences._player.GetComponent<PlayerMaster>();
			ammoBox = GameManagerReferences._player.GetComponent<PlayerAmmoBox>();
		}
	}

	void DecreaseAmmo()
	{
		currentAmmo--;
		UIAmmoUpdateRequest();
	}

	void TryToReload()
	{
		for (int i = 0; i < ammoBox.typesOfAmmunition.Count; i++)
		{
			if (ammoBox.typesOfAmmunition[i].ammoName == ammoName)
			{
				if (ammoBox.typesOfAmmunition[i].ammoCurrentCarried > 0 && currentAmmo != clipSize && !gunMaster.isReloading)
				{
					gunMaster.isReloading = true;
					gunMaster.isGunLoaded = false;
					if (myAnimator != null)
					{
						myAnimator.SetTrigger("Reload");
					}
					else
					{
						StartCoroutine(ReloadWithoutAnimation());
					}
				}
				break;
			}
		}
	}

	void CheckAmmoStatus()
	{
		if (currentAmmo <= 0)
		{
			currentAmmo = 0;
			gunMaster.isGunLoaded = false;
		}
		else if (currentAmmo > 0)
		{
			gunMaster.isGunLoaded = true;
		}
	}

	void StartingSanityCheck()
	{
		if (currentAmmo > clipSize)
		{
			currentAmmo = clipSize;
		}
	}

	void UIAmmoUpdateRequest()
	{
		for (int i = 0; i < ammoBox.typesOfAmmunition.Count; i++)
		{
			if (ammoBox.typesOfAmmunition[i].ammoName == ammoName)
			{
				gunMaster.CallEventAmmoChanged(currentAmmo, ammoBox.typesOfAmmunition[i].ammoCurrentCarried);
			}
		}
	}

	void ResetGunReloading()
	{
		gunMaster.isReloading = false;
		CheckAmmoStatus();
		UIAmmoUpdateRequest();
	}

	public void OnReloadComplete()
	{
		for (int i = 0; i < ammoBox.typesOfAmmunition.Count; i++)
		{
			if (ammoBox.typesOfAmmunition[i].ammoName == ammoName)
			{
				int ammoTopUp = clipSize - currentAmmo;
				if (ammoBox.typesOfAmmunition[i].ammoCurrentCarried >= ammoTopUp)
				{
					currentAmmo += ammoTopUp;
					ammoBox.typesOfAmmunition[i].ammoCurrentCarried -= ammoTopUp;
				}
				else if (ammoBox.typesOfAmmunition[i].ammoCurrentCarried < ammoTopUp && ammoBox.typesOfAmmunition[i].ammoCurrentCarried != 0)
				{
					currentAmmo += ammoBox.typesOfAmmunition[i].ammoCurrentCarried;
					ammoBox.typesOfAmmunition[i].ammoCurrentCarried = 0;
				}
				break;
			}
		}
		ResetGunReloading();
	}

	IEnumerator ReloadWithoutAnimation()
	{
		yield return new WaitForSeconds(reloadTime);
		OnReloadComplete();
	}

	IEnumerator UpdateAmmoUIWhenEnabling()
	{
		yield return new WaitForSeconds(0.05f);
		UIAmmoUpdateRequest();
	}
}
}