using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GunDynamicCrosshair : MonoBehaviour
{

    private GunMaster gunMaster;
    private Transform playerTransform;
    private Transform weaponCamera;
    private Vector3 lastPosition;
    private float playerSpeed;
    private float nextCaptureTime;
    private float captureInterval = 0.5f;

    public Animator crosshairAnimator;
    public Transform canvasDynamicCrosshair;
    public string weaponCameraName;

    void Start()
    {
        SetInitialReferences();
    }

    void Update()
    {
        CapturePlayerSpeed();
        ApplySpeedToAnimation();
    }

    void SetInitialReferences()
    {
        gunMaster = GetComponent<GunMaster>();
        playerTransform = GameManagerReferences._player.transform;
        FindWeaponCamera(playerTransform);
        SetCameraOnDynamicCrosshairCanvas();
        SetPlaneDistanceOnDynamicCrosshairCanvas();
    }

    void CapturePlayerSpeed()
    {
        if (Time.time > nextCaptureTime)
        {
            nextCaptureTime = Time.time + captureInterval;
            playerSpeed = (playerTransform.position - lastPosition).magnitude / captureInterval;
            lastPosition = playerTransform.position;
            gunMaster.CallEventSpeedCaptured(playerSpeed);
        }
    }

    void ApplySpeedToAnimation()
    {
        if (crosshairAnimator != null)
        {
            crosshairAnimator.SetFloat("Speed", playerSpeed);
        }
    }

    void FindWeaponCamera(Transform transformToSearchThrough)
    {
        if (transformToSearchThrough != null)
        {
            if (transformToSearchThrough.name == weaponCameraName)
            {
                weaponCamera = transformToSearchThrough;
                return;
            }
            foreach (Transform child in transformToSearchThrough)
            {
                FindWeaponCamera(child);
            }
        }
    }

    void SetCameraOnDynamicCrosshairCanvas()
    {
        if (canvasDynamicCrosshair != null && weaponCamera != null)
        {
            canvasDynamicCrosshair.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            canvasDynamicCrosshair.GetComponent<Canvas>().worldCamera = weaponCamera.GetComponent<Camera>();
        }
    }

    void SetPlaneDistanceOnDynamicCrosshairCanvas()
    {
        if (canvasDynamicCrosshair != null)
        {
            canvasDynamicCrosshair.GetComponent<Canvas>().planeDistance = 1;
        }
    }
}
}