using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class PlayerCanvasHurt : MonoBehaviour
{

    private PlayerMaster playerMaster;
    private float secondsTillHide = 0.5f;

    public GameObject hurtCanvas;

    void OnEnable()
    {
        SetInitialReferences();
        playerMaster.EventPlayerHealthDecrease += TurnOnHurtEffect;
    }

    void OnDisable()
    {
        playerMaster.EventPlayerHealthDecrease -= TurnOnHurtEffect;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        playerMaster = GetComponent<PlayerMaster>();
    }

    void TurnOnHurtEffect(int dummy)
    {
        if (hurtCanvas != null)
        {
            StopAllCoroutines();
            hurtCanvas.SetActive(true);
            StartCoroutine(ResetHurtCanvas());
        }
    }

    IEnumerator ResetHurtCanvas()
    {
        yield return new WaitForSeconds(secondsTillHide);
        hurtCanvas.SetActive(false);
    }
}
}