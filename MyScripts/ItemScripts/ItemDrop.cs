using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class ItemDrop : MonoBehaviour
{

    private ItemMaster itemMaster;
    private Transform myTransform;

    public string dropButtonName;

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
        CheckForDropInput();
    }

    void SetInitialReferences()
    {
        itemMaster = GetComponent<ItemMaster>();
        myTransform = transform;
    }

    void CheckForDropInput()
    {
        if (Input.GetButtonDown(dropButtonName) && Time.timeScale > 0 && myTransform.root.CompareTag(GameManagerReferences._playerTag))
        {
            myTransform.parent = null;
            itemMaster.CallEventObjectThrow();
        }
    }
}
}