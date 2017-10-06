using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class EnemyTakeDamage : MonoBehaviour
{

    private EnemyMaster enemyMaster;

    public bool shouldRemoveCollider;
    public int damageMultiplier = 1;

    void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventEnemyDie += RemoveThis;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDie -= RemoveThis;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        enemyMaster = transform.root.GetComponent<EnemyMaster>();
    }

    public void ProcessDamage(int damage)
    {
        int damageToApply = damage * damageMultiplier;
        enemyMaster.CallEventEnemyDecreaseHealth(damageToApply);
    }

    void RemoveThis()
    {
        if (shouldRemoveCollider)
        {
            if (GetComponent<Collider>() != null)
            {
                Destroy(GetComponent<Collider>());
            }
            if (GetComponent<Rigidbody>() != null)
            {
                Destroy(GetComponent<Rigidbody>());
            }
        }
        Destroy(this);
    }
}
}