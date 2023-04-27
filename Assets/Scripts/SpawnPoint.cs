using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public delegate void SpawnSetEvent();
    public static event SpawnSetEvent OnSpawnSet;

    // Start is called before the first frame update
    void Start()
    {
        SetSpawn();
    }

    public void SetSpawn()
    {
        MasterScript.spawnPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        Debug.Log("Set spawn to " + MasterScript.spawnPos);

        if (OnSpawnSet != null)
            OnSpawnSet();
    }

}
