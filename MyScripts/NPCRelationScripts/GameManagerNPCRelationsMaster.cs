using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GameManagerNPCRelationsMaster : MonoBehaviour
{
    public delegate void NPCRelationChangeEventHandler(string factAffected, string factCausing, int adjustment, bool chain);
    public delegate void UpdateNPCRelationsEventHandler();
    public event NPCRelationChangeEventHandler EventNPCRelationChange;
    public event UpdateNPCRelationsEventHandler EventUpdateNPCRelationsEverywhere;

    public int hostileThreshold = 40;
    public NPCRelationsArray[] nPCRelationsArray;

    public void CallEventNPCRelationChange(string factionAffected, string factionCausingChange, int relationChangeAmount, bool applyChainEffect)
    {
        if (EventNPCRelationChange != null)
        {
            EventNPCRelationChange(factionAffected, factionCausingChange, relationChangeAmount, applyChainEffect);
        }
    }

    public void CallEventUpdateNPCRelationsEverywhere()
    {
        if (EventUpdateNPCRelationsEverywhere != null)
        {
            EventUpdateNPCRelationsEverywhere();
        }
    }
}
}