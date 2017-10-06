using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPSSystem
{
public class PlayerInventory : MonoBehaviour
{

    private GameManagerToggleInventoryUI inventoryUIScript;
    private PlayerMaster playerMaster;
    private Transform currentlyHeldItem;
    private List<Transform> listInventory = new List<Transform>();
    private float timeToPlaceInHands = 0.1f;
    private int counter;
    private string buttonText;

    public Transform inventoryPlayerParent;
    public Transform inventoryUIParent;
    public GameObject UIButton;

    void OnEnable()
    {
        SetInitialReferences();
        DeactivateAllInventoryItems();
        UpdateInventoryListAndUI();
        CheckIfHandsEmpty();
        playerMaster.EventInventoryChanged += UpdateInventoryListAndUI;
        playerMaster.EventInventoryChanged += CheckIfHandsEmpty;
        playerMaster.EventHandsEmpty += ClearHands;
    }

    void OnDisable()
    {
        playerMaster.EventInventoryChanged -= UpdateInventoryListAndUI;
        playerMaster.EventInventoryChanged -= CheckIfHandsEmpty;
        playerMaster.EventHandsEmpty -= ClearHands;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        inventoryUIScript = FindObjectOfType<GameManagerMaster>().GetComponent<GameManagerToggleInventoryUI>();
        playerMaster = GetComponent<PlayerMaster>();
    }

    void UpdateInventoryListAndUI()
    {
        counter = 0;
        listInventory.Clear();
        listInventory.TrimExcess();
        ClearInventoryUI();
        foreach (Transform child in inventoryPlayerParent)
        {
            if (child.CompareTag("Item"))
            {
                listInventory.Add(child);
                GameObject go = Instantiate(UIButton) as GameObject;
                buttonText = child.name;
                go.GetComponentInChildren<Text>().text = buttonText;
                int index = counter;
                go.GetComponent<Button>().onClick.AddListener(delegate { ActivateInventoryItem(index); });
                go.GetComponent<Button>().onClick.AddListener(inventoryUIScript.ToggleInventoryUI);
                go.transform.SetParent(inventoryUIParent, false);
                counter++;
            }
        }
    }

    void CheckIfHandsEmpty()
    {
        if (currentlyHeldItem == null && listInventory.Count > 0)
        {
            StartCoroutine(PlaceItemInHands(listInventory[listInventory.Count - 1]));
        }
    }

    void ClearHands()
    {
        currentlyHeldItem = null;
    }

    void ClearInventoryUI()
    {
        foreach (Transform child in inventoryUIParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void ActivateInventoryItem(int inventoryIndex)
    {
        DeactivateAllInventoryItems();
        StartCoroutine(PlaceItemInHands(listInventory[inventoryIndex]));
    }

    void DeactivateAllInventoryItems()
    {
        foreach (Transform child in inventoryPlayerParent)
        {
            if (child.CompareTag("Item"))
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator PlaceItemInHands(Transform itemTransform)
    {
        yield return new WaitForSeconds(timeToPlaceInHands);
        currentlyHeldItem = itemTransform;
        currentlyHeldItem.gameObject.SetActive(true);
    }
}
}