using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class DestructibleExplode : MonoBehaviour
{

    private DestructibleMaster destructibleMaster;
    private Transform myTransform;
    private Collider[] struckColliders;
    private RaycastHit hit;
    private float distance;
    private int damageToApply;

    public float explosionRange;
    public float explosionForce;
    public int rawDamage;

    void OnEnable()
    {
        SetInitialReferences();
        destructibleMaster.EventDestroyMe += ExplosionShere;
    }

    void OnDisable()
    {
        destructibleMaster.EventDestroyMe -= ExplosionShere;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        destructibleMaster = GetComponent<DestructibleMaster>();
        myTransform = transform;
    }

    void ExplosionShere()
    {
        myTransform.parent = null;
        GetComponent<Collider>().enabled = false;
        struckColliders = Physics.OverlapSphere(myTransform.position, explosionRange);
        foreach (Collider col in struckColliders)
        {
            distance = Vector3.Distance(myTransform.position, col.transform.position);
            damageToApply = (int)Mathf.Abs((1 - (distance / explosionRange)) * rawDamage);
            if (Physics.Linecast(myTransform.position, col.transform.position, out hit))
            {
                if (hit.transform == col.transform || col.GetComponent<NPCTakeDamage>() != null)
                {
                    col.SendMessage("ProcessDamage", damageToApply, SendMessageOptions.DontRequireReceiver);
                    col.SendMessage("CallEventPlayerHealthDecrease", damageToApply, SendMessageOptions.DontRequireReceiver);
                }
            }
            if (col.GetComponent<Rigidbody>() != null)
            {
                col.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, myTransform.position, explosionRange, 1, ForceMode.Impulse);
            }
        }
        Destroy(gameObject, 0.05f);
    }
}
}