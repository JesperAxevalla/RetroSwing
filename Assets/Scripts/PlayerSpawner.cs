using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private void OnEnable()
    {
        Death.OnDeath += Spawn;
        MasterScript.OnNextLevel += Spawn;
        SpawnPoint.OnSpawnSet += Spawn;
    }

    private void OnDisable()
    {
        Death.OnDeath -= Spawn;
        MasterScript.OnNextLevel -= Spawn;
        SpawnPoint.OnSpawnSet -= Spawn;
    }

    private void Start()
    {
        Spawn();
    }


    public void Spawn()
    {
        Debug.Log("Spawning at " + MasterScript.spawnPos);
        var rb = this.GetComponent<Rigidbody>();
        rb.position = MasterScript.spawnPos;
    }
}
