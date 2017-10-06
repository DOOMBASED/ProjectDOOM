using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public interface NPCStateInterface
{
	void UpdateState();
	void ToPatrolState();
	void ToAlertState();
	void ToPursueState();
	void ToMeleeAttackState();
	void ToRangeAttackState();
}
}