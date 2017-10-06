using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class EnemyMaster : MonoBehaviour
{

	public bool isOnRoute;
	public bool isNavPaused;
	public Transform myTarget;

	public delegate void GeneralEventHandler();
	public delegate void HealthEventHandler(int health);
	public delegate void NavTargetEventHandler(Transform targetTransform);
	public event GeneralEventHandler EventEnemyDie;
	public event GeneralEventHandler EventEnemyWalking;
	public event GeneralEventHandler EventEnemyReachedNavTarget;
	public event GeneralEventHandler EventEnemyAttack;
	public event GeneralEventHandler EventEnemyLostTarget;
	public event GeneralEventHandler EventEnemyHealthLow;
	public event GeneralEventHandler EventEnemyHealthRecovered;
	public event HealthEventHandler EventEnemyDecreaseHealth;
	public event HealthEventHandler EventEnemyIncreaseHealth;
	public event NavTargetEventHandler EventEnemySetNavTarget;

	public void CallEventEnemyDecreaseHealth(int health)
	{
		if (EventEnemyDecreaseHealth != null)
		{
			EventEnemyDecreaseHealth(health);
		}
	}

	public void CallEventEnemyIncreaseHealth(int health)
	{
		if (EventEnemyIncreaseHealth != null)
		{
			EventEnemyIncreaseHealth(health);
		}
	}

	public void CallEventEnemySetNavTarget(Transform targTransform)
	{
		if (EventEnemySetNavTarget != null)
		{
			EventEnemySetNavTarget(targTransform);
		}
		myTarget = targTransform;
	}

	public void CallEventEnemyDie()
	{
		if (EventEnemyDie != null)
		{
			EventEnemyDie();
		}
	}

	public void CallEventEnemyWalking()
	{
		if (EventEnemyWalking != null)
		{
			EventEnemyWalking();
		}
	}

	public void CallEventEnemyReachedNavTarget()
	{
		if (EventEnemyReachedNavTarget != null)
		{
			EventEnemyReachedNavTarget();
		}
	}

	public void CallEventEnemyAttack()
	{
		if (EventEnemyAttack != null)
		{
			EventEnemyAttack();
		}
	}

	public void CallEventEnemyLostTarget()
	{
		if (EventEnemyLostTarget != null)
		{
			EventEnemyLostTarget();
		}
		myTarget = null;
	}

	public void CallEventEnemyHealthLow()
	{
		if (EventEnemyHealthLow != null)
		{
			EventEnemyHealthLow();
		}
	}

	public void CallEventEnemyHealthRecovered()
	{
		if (EventEnemyHealthRecovered != null)
		{
			EventEnemyHealthRecovered();
		}
	}
}
}