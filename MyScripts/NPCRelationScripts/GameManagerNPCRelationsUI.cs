using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPSSystem
{
public class GameManagerNPCRelationsUI : MonoBehaviour
{

    private GameManagerNPCRelationsMaster nPCRelationsMaster;
    private GameManagerMaster gameManagerMaster;

    public Transform panelFactionRelations;
    public GameObject panelRelationsPrefab;
    public GameObject labelRelationsPrefab;

    void OnEnable()
    {
        SetInitialReferences();
        DrawUI();
        gameManagerMaster.MenuToggleEvent += DrawUI;
    }

    void OnDisable()
    {
        gameManagerMaster.MenuToggleEvent -= DrawUI;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        nPCRelationsMaster = GetComponent<GameManagerNPCRelationsMaster>();
        gameManagerMaster = GetComponent<GameManagerMaster>();
    }

    void DrawUI()
    {
        ClearUI();
        foreach (NPCRelationsArray npcArray in nPCRelationsMaster.nPCRelationsArray)
        {
            GameObject panelRelation = Instantiate(panelRelationsPrefab) as GameObject;
            panelRelation.transform.SetParent(panelFactionRelations, false);
            GameObject headerLabel = Instantiate(labelRelationsPrefab) as GameObject;
            headerLabel.transform.SetParent(panelRelation.transform, false);
            headerLabel.GetComponentInChildren<Text>().text = npcArray.nPCFaction;
            foreach (NPCRelations nPCRelation in npcArray.nPCRelations)
            {
                GameObject labelRelations = Instantiate(labelRelationsPrefab) as GameObject;
                labelRelations.transform.SetParent(panelRelation.transform, false);
                labelRelations.GetComponentInChildren<Text>().text = nPCRelation.nPCTag + "\n" + nPCRelation.nPCFactionRating.ToString();
            }
        }
    }

    void ClearUI()
    {
        if (panelFactionRelations.transform.childCount == 0)
        {
            return;
        }
        foreach (Transform panelRelation in panelFactionRelations)
        {
            Destroy(panelRelation.gameObject);
        }
    }
}
}