using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class ItemSounds : MonoBehaviour
{

    private ItemMaster itemMaster;

    public AudioClip throwSound;
    public AudioClip pickupSound;
    public float defaultVolume;

    void OnEnable()
    {
        SetInitialReferences();
        itemMaster.EventObjectThrow += PlayThrowSound;
        itemMaster.EventObjectPickup += PlayPickupSound;
    }

    void OnDisable()
    {
        itemMaster.EventObjectThrow -= PlayThrowSound;
        itemMaster.EventObjectPickup -= PlayPickupSound;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        itemMaster = GetComponent<ItemMaster>();
    }

    void PlayThrowSound()
    {
        if (throwSound != null)
        {
            AudioSource.PlayClipAtPoint(throwSound, transform.position, defaultVolume);
        }
    }

    void PlayPickupSound()
    {
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, defaultVolume);
        }
    }
}
}