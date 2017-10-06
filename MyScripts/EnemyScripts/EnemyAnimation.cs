using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class EnemyAnimation : MonoBehaviour
{

    private EnemyMaster enemyMaster;
    private Animator myAnimator;

    void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventEnemyDie += DisableAnimator;
        enemyMaster.EventEnemyWalking += SetAnimationToWalk;
        enemyMaster.EventEnemyReachedNavTarget += SetAnimationToIdle;
        enemyMaster.EventEnemyAttack += SetAnimationToAttack;
        enemyMaster.EventEnemyDecreaseHealth += SetAnimationToStruck;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDie -= DisableAnimator;
        enemyMaster.EventEnemyWalking -= SetAnimationToWalk;
        enemyMaster.EventEnemyReachedNavTarget -= SetAnimationToIdle;
        enemyMaster.EventEnemyAttack -= SetAnimationToAttack;
        enemyMaster.EventEnemyDecreaseHealth -= SetAnimationToStruck;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        enemyMaster = GetComponent<EnemyMaster>();
        if (GetComponent<Animator>() != null)
        {
            myAnimator = GetComponent<Animator>();
        }
    }

    void SetAnimationToWalk()
    {
        if (myAnimator != null)
        {
            if (myAnimator.enabled)
            {
                myAnimator.SetBool("isPursuing", true);
            }
        }
    }

    void SetAnimationToIdle()
    {
        if (myAnimator != null)
        {
            if (myAnimator.enabled)
            {
                myAnimator.SetBool("isPursuing", false);
            }
        }
    }

    void SetAnimationToAttack()
    {
        if (myAnimator != null)
        {
            if (myAnimator.enabled)
            {
                myAnimator.SetTrigger("Attack");
            }
        }
    }

    void SetAnimationToStruck(int dummy)
    {
        if (myAnimator != null)
        {
            if (myAnimator.enabled)
            {
                myAnimator.SetTrigger("Struck");
            }
        }
    }

    void DisableAnimator()
    {
        if (myAnimator != null)
        {
            myAnimator.enabled = false;
        }
    }
}
}