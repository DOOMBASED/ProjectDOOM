using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class MeleeStrike : MonoBehaviour
{

    private MeleeMaster meleeMaster;
    private float nextSwingTime;

    public int damage = 10;
    
    void OnEnable()
    {
        SetInitialReferences();
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        meleeMaster = GetComponent<MeleeMaster>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != GameManagerReferences._player && meleeMaster.isInUse && Time.time > nextSwingTime)
        //if (collision.gameObject != GameManagerReferences._player && meleeMaster.isInUse)
        {
            nextSwingTime = Time.time + meleeMaster.swingRate;
            collision.transform.SendMessage("ProcessDamage", damage, SendMessageOptions.DontRequireReceiver);
            collision.transform.root.SendMessage("SetMyAttacker", transform.root, SendMessageOptions.DontRequireReceiver);
            meleeMaster.CallEventHit(collision, collision.transform);
        }
    }
}
}