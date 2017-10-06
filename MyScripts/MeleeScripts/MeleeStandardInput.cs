using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class MeleeStandardInput : MonoBehaviour
{

    private MeleeMaster meleeMaster;
    private Transform myTransform;
    private float nextSwing;

    public string attackButtonName;

    void OnEnable()
    {
        SetInitialReferences();
    }

    void Start()
    {
        SetInitialReferences();
    }

    void Update()
    {
        CheckIfWeaponShouldAttack();
    }

    void SetInitialReferences()
    {
        meleeMaster = GetComponent<MeleeMaster>();
        myTransform = transform;
    }

    void CheckIfWeaponShouldAttack()
    {
        if (Time.timeScale > 0 && myTransform.root.CompareTag(GameManagerReferences._playerTag) && !meleeMaster.isInUse)
        {
            if (Input.GetButton(attackButtonName) || (Mathf.Round(Input.GetAxisRaw(attackButtonName)) < 0) && Time.time > nextSwing)
            {
                nextSwing = Time.time + meleeMaster.swingRate;
                meleeMaster.isInUse = true;
                meleeMaster.CallEventPlayerInput();
            }
        }
    }
}
}