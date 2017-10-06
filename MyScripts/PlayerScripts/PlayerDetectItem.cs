using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class PlayerDetectItem : MonoBehaviour
{

    private Transform itemAvailableForPickup;
    private RaycastHit hit;
    private float detectRange = 3;
    private float detectRadius = 0.7f;
    private float labelWidth = 200;
    private float labelHeight = 50;
    private bool itemInRange;

    public Transform rayTransformPivot;
    public LayerMask layerToDetect;
    public string buttonPickup;

    void Update()
    {
        CastRayForDetectingItems();
        CheckForItemPickupAttempt();
    }

    void CastRayForDetectingItems()
    {
        if (Physics.SphereCast(rayTransformPivot.position, detectRadius, rayTransformPivot.forward, out hit, detectRange, layerToDetect))
        {
            itemAvailableForPickup = hit.transform;
            itemInRange = true;
        }
        else
        {
            itemInRange = false;
        }
    }

    void CheckForItemPickupAttempt()
    {
        if (Input.GetButtonDown(buttonPickup) && Time.timeScale > 0 && itemInRange && itemAvailableForPickup.root.tag != GameManagerReferences._playerTag)
        {
            itemAvailableForPickup.GetComponent<ItemMaster>().CallEventPickupAction(rayTransformPivot);
        }
    }

    void OnGUI()
    {
        if (itemInRange && itemAvailableForPickup != null)
        {
            GUI.Label(new Rect(Screen.width / 2 - labelWidth / 2, Screen.height / 2, labelWidth, labelHeight), itemAvailableForPickup.name);
        }
    }
}
}