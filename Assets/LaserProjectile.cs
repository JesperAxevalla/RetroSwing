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

    private void OnEnable()
    {
        Win.OnLevelFinish += LaserHit;
    }

    private void OnDisable()
    {
        Win.OnLevelFinish -= LaserHit;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.isTrigger)
        {

            if (collision.gameObject.tag == "Player")
                Death.KillPlayer();

            LaserHit();
        }


    }

    void LaserHit()
    {
        Destroy(this.gameObject);
    }

}
