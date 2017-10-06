using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FPSSystem
{
public class NPCStatePattern : MonoBehaviour
{
    private float checkRate = 0.1f;
    private float nextCheck;

    [HideInInspector]
    public NavMeshAgent myNavMeshAgent;
    [HideInInspector]
    public Transform pursueTarget;
    [HideInInspector]
    public Vector3 locationOfInterest;
    [HideInInspector]
    public Vector3 wanderTarget;
    public NPCMaster nPCMaster;
    public MeshRenderer meshRendererFlag;
    public Transform myFollowTarget;
    public Transform myAttacker;
    public Transform head;
    public GameObject rangeWeapon;
    public LayerMask sightLayers;
    public LayerMask myEnemyLayers;
    public LayerMask myFriendlyLayers;
    public Transform[] waypoints;
    public string[] myEnemyTags;
    public string[] myFriendlyTags;
    public int requiredDetectionCount = 15;
    public float sightRange = 40;
    public float detectBehindRange = 5;
    public float fleeRange = 25;
    public float offset = 0.4f;
    public float attackRate = 0.4f;
    public float nextAttack;
    public float rangeAttackRange = 35;
    public float rangeAttackDamage = 5;
    public float rangeAttackSpread = 0.5f;
    public float meleeAttackRange = 4;
    public float meleeAttackDamage = 10;
    public bool hasRangeAttack;
    public bool hasMeleeAttack;
    public bool isMeleeAttacking;
    public NPCStateInterface currentState;
    public NPCStateInterface capturedState;
    public NPCStatePatrol patrolState;
    public NPCStateAlert alertState;
    public NPCStatePursue pursueState;
    public NPCStateMeleeAttack meleeAttackState;
    public NPCStateRangeAttack rangeAttackState;
    public NPCStateFlee fleeState;
    public NPCStateStruck struckState;
    public NPCStateInvestigateHarm investigateHarmState;
    public NPCStateFollow followState;

    void Awake()
    {
        SetupStateReferences();
        SetInitialReferences();
        nPCMaster.EventNPCLowHealth += ActivateFleeState;
        nPCMaster.EventNPCHealthRecovered += ActivatePatrolState;
        nPCMaster.EventNPCDecreaseHealth += ActivateStruckState;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void OnDisable()
    {
        nPCMaster.EventNPCLowHealth -= ActivateFleeState;
        nPCMaster.EventNPCHealthRecovered -= ActivatePatrolState;
        nPCMaster.EventNPCDecreaseHealth -= ActivateStruckState;
        StopAllCoroutines();
    }

    void Update()
    {
        CarryOutUpdateState();
    }

    void SetupStateReferences()
    {
        patrolState = new NPCStatePatrol(this);
        alertState = new NPCStateAlert(this);
        pursueState = new NPCStatePursue(this);
        fleeState = new NPCStateFlee(this);
        followState = new NPCStateFollow(this);
        meleeAttackState = new NPCStateMeleeAttack(this);
        rangeAttackState = new NPCStateRangeAttack(this);
        struckState = new NPCStateStruck(this);
        investigateHarmState = new NPCStateInvestigateHarm(this);
    }

    void SetInitialReferences()
    {
        nPCMaster = GetComponent<NPCMaster>();
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        ActivatePatrolState();
    }

    void CarryOutUpdateState()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            currentState.UpdateState();
        }
    }

    void ActivatePatrolState()
    {
        currentState = patrolState;
    }

    void ActivateFleeState()
    {
        if (currentState == struckState)
        {
            capturedState = fleeState;
            return;
        }
        currentState = fleeState;
    }

    void ActivateStruckState(int dummy)
    {
        StopAllCoroutines();
        if (currentState != struckState)
        {
            capturedState = currentState;
        }
        if (rangeWeapon != null)
        {
            rangeWeapon.SetActive(false);
        }
        if (myNavMeshAgent.enabled)
        {
            myNavMeshAgent.isStopped = true;
        }
        currentState = struckState;
        isMeleeAttacking = false;
        nPCMaster.CallEventNPCStruckAnim();
        StartCoroutine(RecoverFromStruckState());
    }

    IEnumerator RecoverFromStruckState()
    {
        yield return new WaitForSeconds(1.5f);
        nPCMaster.CallEventNPCRecoveredAnim();
        if (rangeWeapon != null)
        {
            rangeWeapon.SetActive(true);
        }
        if (myNavMeshAgent.enabled)
        {
            myNavMeshAgent.isStopped = false;
        }
        currentState = capturedState;
    }

    public void OnEnemyAttack()
    {
        if (pursueTarget != null)
        {
            if (Vector3.Distance(transform.position, pursueTarget.position) <= meleeAttackRange)
            {
                Vector3 toOther = pursueTarget.position - transform.position;
                if (Vector3.Dot(toOther, transform.forward) > 0.5f)
                {
                    pursueTarget.SendMessage("CallEventPlayerHealthDecrease", meleeAttackDamage, SendMessageOptions.DontRequireReceiver);
                    pursueTarget.SendMessage("ProcessDamage", meleeAttackDamage, SendMessageOptions.DontRequireReceiver);
                    pursueTarget.SendMessage("SetMyAttacker", transform.root, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        isMeleeAttacking = false;
    }

    //public void SetMyAttacker(Transform attacker)
    //{
    //    myAttacker = attacker;
    //}

    public void Distract(Vector3 distractionPos)
    {
        locationOfInterest = distractionPos;
        if (currentState == patrolState)
        {
            currentState = alertState;
        }
    }
}
}