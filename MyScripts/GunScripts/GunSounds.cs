using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GunSounds : MonoBehaviour
{

	private GunMaster gunMaster;
	private Transform myTransform;

	public AudioClip[] shootSound;
	public AudioClip reloadSound;
	public float shootVolume = 0.4f;
	public float reloadVolume = 0.5f;

	void OnEnable()
	{
		SetInitialReferences();
		gunMaster.EventPlayerInput += PlayShootSound;
		gunMaster.EventNPCInput += NPCPlayShootSound;
	}

	void OnDisable()
	{
		gunMaster.EventPlayerInput -= PlayShootSound;
		gunMaster.EventNPCInput -= NPCPlayShootSound;
	}

	void Start()
	{
		SetInitialReferences();
	}

	void SetInitialReferences()
	{
		gunMaster = GetComponent<GunMaster>();
		myTransform = transform;
	}

	void PlayShootSound()
	{
		if (shootSound.Length > 0)
		{
			int index = Random.Range(0, shootSound.Length);
			AudioSource.PlayClipAtPoint(shootSound[index], myTransform.position, shootVolume);
		}
	}

	public void PlayReloadSound()
	{
		if (reloadSound != null)
		{
			AudioSource.PlayClipAtPoint(reloadSound, myTransform.position, reloadVolume);
		}
	}

	void NPCPlayShootSound(float dummy)
	{
		PlayShootSound();
	}
}
}