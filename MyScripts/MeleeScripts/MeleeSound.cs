using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class MeleeSound : MonoBehaviour
{

    private MeleeMaster meleeMaster;
    private Transform myTransform;

    public AudioClip swingSound;
    public AudioClip strikeSound;
    public float swingSoundVolume = 0.5f;
    public float strikeSoundVolume = 0.5f;

    void OnEnable()
    {
        SetInitialReferences();
        meleeMaster.EventHit += PlayStrikeSound;
    }

    void OnDisable()
    {
        meleeMaster.EventHit -= PlayStrikeSound;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        meleeMaster = GetComponent<MeleeMaster>();
        myTransform = transform;
    }

    void PlaySwingSound()
    {
        if (swingSound != null)
        {
            AudioSource.PlayClipAtPoint(swingSound, myTransform.position, swingSoundVolume);
        }
    }

    void PlayStrikeSound(Collision dummyCol, Transform dummyTrans)
    {
        if (strikeSound != null)
        {
            AudioSource.PlayClipAtPoint(strikeSound, myTransform.position, strikeSoundVolume);
        }
    }
}
}