using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private GameObject body;

    // Start is called before the first frame update
    void Start()
    {

        var rb = GetComponent<Rigidbody>();

        rb.velocity = this.transform.rotation * Vector3.forward * speed;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 6)
            Death.KillPlayer();

        Debug.Log("HIT");

        Destroy(this,0.1f);

    }
}
