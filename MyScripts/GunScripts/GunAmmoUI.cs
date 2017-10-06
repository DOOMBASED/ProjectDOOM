using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPSSystem
{
public class GunAmmoUI : MonoBehaviour
{

    private GunMaster gunMaster;

    public InputField currentAmmoField;
    public InputField carriedAmmoField;

    void OnEnable()
    {
        SetInitialReferences();
        gunMaster.EventAmmoChanged += UpdateAmmoUI;
    }

    void OnDisable()
    {
        gunMaster.EventAmmoChanged -= UpdateAmmoUI;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        gunMaster = GetComponent<GunMaster>();
    }

    void UpdateAmmoUI(int currentAmmo, int carriedAmmo)
    {
        if (currentAmmoField != null)
        {
            currentAmmoField.text = currentAmmo.ToString();
        }
        if (carriedAmmoField != null)
        {
            carriedAmmoField.text = carriedAmmo.ToString();
        }
    }
}
}