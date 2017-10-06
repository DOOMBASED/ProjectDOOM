using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class DestructibleSound : MonoBehaviour
{

    private DestructibleMaster destructibleMaster;

    public AudioClip explosionSound;
    public float explosionVolume = 0.5f;

    void OnEnable()
    {
        SetInitialReferences();
        destructibleMaster.EventDestroyMe += PlayExplosionSound;
    }

    void OnDisable()
    {
        destructibleMaster.EventDestroyMe -= PlayExplosionSound;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        destructibleMaster = GetComponent<DestructibleMaster>();
    }

    void PlayExplosionSound()
    {
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position, explosionVolume);
        }
    }
}
}