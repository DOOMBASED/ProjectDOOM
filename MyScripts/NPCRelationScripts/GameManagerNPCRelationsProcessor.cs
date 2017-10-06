using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GameManagerNPCRelationsProcessor : MonoBehaviour
{

    private GameManagerNPCRelationsMaster nPCRelationsMaster;

    void OnEnable()
    {
        SetInitialReferences();
        nPCRelationsMaster.EventNPCRelationChange += ProcessFactionRelations;
        FillFriendlyAndEnemyTags();
        SetFriendlyAndEnemyLayer();
        UpdateNPCRelationsEverywhere();
    }

    void OnDisable()
    {
        nPCRelationsMaster.EventNPCRelationChange -= ProcessFactionRelations;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        nPCRelationsMaster = GetComponent<GameManagerNPCRelationsMaster>();
    }

    void ProcessFactionRelations(string factionAffected, string factionInstigating, int relationChange, bool applyChainEffect)
    {
        if (nPCRelationsMaster.nPCRelationsArray == null)
        {
            return;
        }
        foreach (NPCRelationsArray nPCArray in nPCRelationsMaster.nPCRelationsArray)
        {
            if (nPCArray.nPCFaction == factionInstigating)
            {
                foreach (NPCRelations nPCRelation in nPCArray.nPCRelations)
                {
                    if (nPCRelation.nPCTag == factionAffected)
                    {
                        nPCRelation.nPCFactionRating += relationChange;
                        break;
                    }
                }
            }
            if (nPCArray.nPCFaction == factionAffected)
            {
                foreach (NPCRelations nPCRelation in nPCArray.nPCRelations)
                {
                    if (nPCRelation.nPCTag == factionInstigating)
                    {
                        nPCRelation.nPCFactionRating += relationChange;
                        break;
                    }
                }
            }
            if (nPCArray.nPCFaction != factionAffected && nPCArray.nPCFaction != factionInstigating && applyChainEffect)
            {
                foreach (NPCRelations nPCRelation in nPCArray.nPCRelations)
                {
                    if (nPCRelation.nPCTag == factionAffected)
                    {
                        if (nPCRelation.nPCFactionRating > nPCRelationsMaster.hostileThreshold)
                        {
                            foreach (NPCRelations nPCRel in nPCArray.nPCRelations)
                            {
                                if (nPCRel.nPCTag == factionInstigating)
                                {
                                    nPCRel.nPCFactionRating += relationChange;
                                    EditInstigatorRelationWithObserver(nPCArray.nPCFaction, factionInstigating, relationChange);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (NPCRelations nPCRel in nPCArray.nPCRelations)
                            {
                                if (nPCRel.nPCTag == factionInstigating)
                                {
                                    nPCRel.nPCFactionRating += -relationChange;
                                    EditInstigatorRelationWithObserver(nPCArray.nPCFaction, factionInstigating, -relationChange);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            FillFriendlyAndEnemyTags();
            SetFriendlyAndEnemyLayer();
            UpdateNPCRelationsEverywhere();
        }
    }

    void EditInstigatorRelationWithObserver(string observingNPCFaction, string instigatorFaction, int relationChange)
    {
        foreach (NPCRelationsArray nPCArray in nPCRelationsMaster.nPCRelationsArray)
        {
            if (nPCArray.nPCFaction == instigatorFaction)
            {
                foreach (NPCRelations nPCRelation in nPCArray.nPCRelations)
                {
                    if (nPCRelation.nPCTag == observingNPCFaction)
                    {
                        nPCRelation.nPCFactionRating += relationChange;
                        break;
                    }
                }
            }
        }
    }

    void FillFriendlyAndEnemyTags()
    {
        if (nPCRelationsMaster.nPCRelationsArray == null)
        {
            return;
        }
        foreach (NPCRelationsArray nPCArray in nPCRelationsMaster.nPCRelationsArray)
        {
            List<string> tmpFriendlyTags = new List<string>();
            List<string> tmpEnemyTags = new List<string>();
            foreach (NPCRelations nPCRelation in nPCArray.nPCRelations)
            {
                if (nPCRelation.nPCFactionRating > nPCRelationsMaster.hostileThreshold)
                {
                    tmpFriendlyTags.Add(nPCRelation.nPCTag);
                }
                else
                {
                    tmpEnemyTags.Add(nPCRelation.nPCTag);
                }
            }
            nPCArray.myFriendlyTags = tmpFriendlyTags.ToArray();
            nPCArray.myEnemyTags = tmpEnemyTags.ToArray();
        }
    }

    void SetFriendlyAndEnemyLayer()
    {
        if (nPCRelationsMaster.nPCRelationsArray == null)
        {
            return;
        }
        foreach (NPCRelationsArray nPCArray in nPCRelationsMaster.nPCRelationsArray)
        {
            LayerMask tmpFirendly = new LayerMask();
            LayerMask tmpEnemy = new LayerMask();
            foreach (NPCRelations nPCRelation in nPCArray.nPCRelations)
            {
                if (nPCRelation.nPCFactionRating > nPCRelationsMaster.hostileThreshold)
                {
                    tmpFirendly |= 1 << LayerMask.NameToLayer(nPCRelation.nPCTag);
                }
                else
                {
                    tmpEnemy |= 1 << LayerMask.NameToLayer(nPCRelation.nPCTag);
                }
            }
            nPCArray.myFriendlyLayers = tmpFirendly;
            nPCArray.myEnemyLayers = tmpEnemy;
        }
    }

    void UpdateNPCRelationsEverywhere()
    {
        nPCRelationsMaster.CallEventUpdateNPCRelationsEverywhere();
    }
}
}