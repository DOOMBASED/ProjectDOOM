using UnityEngine;

[System.Serializable]
public class NPCRelations
{

    public string nPCTag;
    public int nPCFactionRating;

    NPCRelations(string nPCStringTag, int nPCIntFactionRating)
    {
        nPCTag = nPCStringTag;
        nPCFactionRating = nPCIntFactionRating;
    }
}

[System.Serializable]
public class NPCRelationsArray
{
    public string nPCFaction;
    public LayerMask myFriendlyLayers;
    public LayerMask myEnemyLayers;
    public NPCRelations[] nPCRelations;
    public string[] myFriendlyTags;
    public string[] myEnemyTags;

    NPCRelationsArray(string nPCStringTagOfInterest, NPCRelations[] nPCRelationsArray, string[] fTags, string[] eTags, LayerMask fLayers, LayerMask eLayers)
    {
        nPCFaction = nPCStringTagOfInterest;
        nPCRelations = nPCRelationsArray;
        myFriendlyTags = fTags;
        myEnemyTags = eTags;
        myFriendlyLayers = fLayers;
        myEnemyLayers = eLayers;
    }
}