using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{

    [SerializeField]
    private float force = 25f;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 6)
            return;

        GameObject player = collision.gameObject;

        Rigidbody rb = player.GetComponent<Rigidbody>();

        rb.AddForce(this.transform.up * force, ForceMode.Impulse);
    }
}
