using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class SpawnerProximity : MonoBehaviour
{

    private Transform myTransform;
    private Transform playerTransform;
    private Vector3 spawnPosition;
    private float checkRate;
    private float nextCheck;

    public GameObject objectToSpawn;
    public int numberToSpawn;
    public float proximity;
    public Transform[] waypoints;

    void Start()
    {
        SetInitialReferences();
    }

    void Update()
    {
        CheckDistance();
    }

    void SetInitialReferences()
    {
        myTransform = transform;
        playerTransform = GameManagerReferences._player.transform;
        checkRate = Random.Range(0.8f, 1.2f);
    }

    void CheckDistance()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            if (Vector3.Distance(myTransform.position, playerTransform.position) < proximity)
            {
                SpawnObjects();
                this.enabled = false;
            }
        }
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            spawnPosition = myTransform.position + Random.insideUnitSphere * 5;
            //Instantiate(objectToSpawn, spawnPosition, myTransform.rotation);
            GameObject go = (GameObject)Instantiate(objectToSpawn, spawnPosition, myTransform.rotation);
            if (waypoints.Length > 0)
            {
                if (go.GetComponent<NPCStatePattern>() != null)
                {
                    go.GetComponent<NPCStatePattern>().waypoints = waypoints;
                }
            }

        }
    }
}
}