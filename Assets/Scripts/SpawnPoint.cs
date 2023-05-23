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
        MasterScript.spawnRot = this.transform.rotation;

        if (OnSpawnSet != null)
            OnSpawnSet();
    }

    public void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.color = Color.red;
        //Vector3 dirPos = this.gameObject.transform.position;
        //dirPos.x -= 1f;
        //dirPos.y += 0.5f;
        Gizmos.DrawCube(new Vector3(0,0,1), new Vector3(-0.2f, -0.2f, 2f));

        Gizmos.color = Color.green;
        Gizmos.DrawCube(Vector3.zero, new Vector3(1f, 2f, 1f));
    }

}
