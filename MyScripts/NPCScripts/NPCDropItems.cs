using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class NPCDropItems : MonoBehaviour
{

    private NPCMaster nPCMaster;

    public GameObject[] itemsToDrop;

    void OnEnable()
    {
        SetInitialReferences();
        nPCMaster.EventNPCDie += DropItems;
    }

    void OnDisable()
    {
        nPCMaster.EventNPCDie -= DropItems;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        nPCMaster = GetComponent<NPCMaster>();
    }

    void DropItems()
    {
        if (itemsToDrop.Length > 0)
        {
            foreach (GameObject item in itemsToDrop)
            {
                StartCoroutine(PauseBeforeDrop(item));
            }
        }
    }

    IEnumerator PauseBeforeDrop(GameObject itemToDrop)
    {
        yield return new WaitForSeconds(0.05f);
        if (itemToDrop.GetComponent<Animator>() != null)
        {
            itemToDrop.GetComponent<Animator>().enabled = false;
        }
        itemToDrop.SetActive(true);
        itemToDrop.transform.parent = null;
        yield return new WaitForSeconds(0.05f);
        if (itemToDrop.GetComponent<ItemMaster>() != null)
        {
            itemToDrop.GetComponent<ItemMaster>().CallEventObjectThrow();
        }
    }
}
}