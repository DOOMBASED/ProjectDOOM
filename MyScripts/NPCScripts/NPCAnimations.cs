using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class NPCAnimations : MonoBehaviour
{

	private NPCMaster nPCMaster;
	private Animator myAnimator;

	void OnEnable()
	{
		SetInitialReferences();
		nPCMaster.EventNPCAttackAnim += ActivateAttackAnimation;
		nPCMaster.EventNPCWalkAnim += ActivateWalkingAnimation;
		nPCMaster.EventNPCIdleAnim += ActivateIdleAnimation;
		nPCMaster.EventNPCRecoveredAnim += ActivateRecoveredAnimation;
		nPCMaster.EventNPCStruckAnim += ActivateStruckAnimation;
	}

	void OnDisable()
	{
		nPCMaster.EventNPCAttackAnim -= ActivateAttackAnimation;
		nPCMaster.EventNPCWalkAnim -= ActivateWalkingAnimation;
		nPCMaster.EventNPCIdleAnim -= ActivateIdleAnimation;
		nPCMaster.EventNPCRecoveredAnim -= ActivateRecoveredAnimation;
		nPCMaster.EventNPCStruckAnim -= ActivateStruckAnimation;
	}

	void Start()
	{
		SetInitialReferences();
	}

	void SetInitialReferences()
	{
		nPCMaster = GetComponent<NPCMaster>();
		if (GetComponent<Animator>() != null)
		{
			myAnimator = GetComponent<Animator>();
		}
	}

	void ActivateWalkingAnimation()
	{
		if (myAnimator != null)
		{
			if (myAnimator.enabled)
			{
				myAnimator.SetBool(nPCMaster.animationBoolPursuing, true);
			}
		}
	}

	void ActivateIdleAnimation()
	{
		if (myAnimator != null)
		{
			if (myAnimator.enabled)
			{
				myAnimator.SetBool(nPCMaster.animationBoolPursuing, false);
			}
		}
	}

	void ActivateAttackAnimation()
	{
		if (myAnimator != null)
		{
			if (myAnimator.enabled)
			{
				myAnimator.SetTrigger(nPCMaster.animationTriggerMelee);
			}
		}
	}

	void ActivateRecoveredAnimation()
	{
		if (myAnimator != null)
		{
			if (myAnimator.enabled)
			{
				myAnimator.SetTrigger(nPCMaster.animationTriggerRecovered);
			}
		}
	}

	void ActivateStruckAnimation()
	{
		if (myAnimator != null)
		{
			if (myAnimator.enabled)
			{
				myAnimator.SetTrigger(nPCMaster.animationTriggerStruck);
			}
		}
	}
}
}