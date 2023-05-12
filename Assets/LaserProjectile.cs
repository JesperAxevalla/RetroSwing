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
        if (collision.gameObject.tag == "Player")
            Death.KillPlayer();

        Debug.Log("LASER HIT: " + collision.gameObject.tag + " LAYER: " + collision.gameObject.layer + " GAME OBJ: "+ collision.gameObject.name);

        Destroy(this.gameObject,0.1f);

    }
}
