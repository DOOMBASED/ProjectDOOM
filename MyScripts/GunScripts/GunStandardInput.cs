using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GunStandardInput : MonoBehaviour
{

    private GunMaster gunMaster;
    private Transform myTransform;
    private bool isBurstFireActive;
    private float nextAttack;

    public bool isAutomatic;
    public bool hasBurstFire;
    public float attackRate = 0.5f;
    public string attackButtonName;
    public string reloadButtonName;
    public string burstFireButtonName;

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
        CheckForBurstFireToggle();
        CheckForReloadRequest();
    }

    void SetInitialReferences()
    {
        gunMaster = GetComponent<GunMaster>();
        myTransform = transform;
        gunMaster.isGunLoaded = true;
    }

    void CheckIfWeaponShouldAttack()
    {
        if (Time.time > nextAttack && Time.timeScale > 0 && myTransform.root.CompareTag(GameManagerReferences._playerTag))
        {
            if (isAutomatic && !isBurstFireActive)
            {
                if (Input.GetButton(attackButtonName) || (Mathf.Round(Input.GetAxisRaw(attackButtonName)) == -1))
                {
                    AttemptAttack();
                }
            }
            else if (isAutomatic && isBurstFireActive)
            {
                if (Input.GetButtonDown(attackButtonName) || (Mathf.Round(Input.GetAxisRaw(attackButtonName)) == -1))
                {
                    StartCoroutine(RunBurstFire());
                }
            }
            else if (!isAutomatic)
            {
                if (Input.GetButton(attackButtonName) || (Mathf.Round(Input.GetAxisRaw(attackButtonName)) == -1))
                {
                    AttemptAttack();
                }
            }
        }
    }

    void AttemptAttack()
    {
        nextAttack = Time.time + attackRate;
        if (gunMaster.isGunLoaded)
        {
            gunMaster.CallEventPlayerInput();
        }
        else
        {
            gunMaster.CallEventGunNotUsable();
        }
    }

    void CheckForReloadRequest()
    {
        if (Input.GetButtonDown(reloadButtonName) && Time.timeScale > 0 && myTransform.root.CompareTag(GameManagerReferences._playerTag))
        {
            gunMaster.CallEventRequestReload();
        }
    }

    void CheckForBurstFireToggle()
    {
        if (Input.GetButtonDown(burstFireButtonName) && Time.timeScale > 0 && myTransform.root.CompareTag(GameManagerReferences._playerTag))
        {
            isBurstFireActive = !isBurstFireActive;
            gunMaster.CallEventToggleBurstFire();
        }
    }

    IEnumerator RunBurstFire()
    {
        AttemptAttack();
        yield return new WaitForSeconds(attackRate);
        AttemptAttack();
        yield return new WaitForSeconds(attackRate);
        AttemptAttack();
        yield return new WaitForSeconds(attackRate);
    }
}
}