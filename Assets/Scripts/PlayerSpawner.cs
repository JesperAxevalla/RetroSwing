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
        var rb = this.GetComponent<Rigidbody>();
        var ch = this.GetComponent<PlayerCam>();
        ch.yRotation = MasterScript.spawnRot.eulerAngles.y;
        Debug.Log("Spawning at " + MasterScript.spawnPos + " ROT: " + MasterScript.spawnRot.eulerAngles);
        ch.xRotation = 0f;
        this.transform.rotation = MasterScript.spawnRot;
        rb.position = MasterScript.spawnPos;
    }
}
