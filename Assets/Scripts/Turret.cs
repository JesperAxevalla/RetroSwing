using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private GameObject tip;
    [SerializeField]
    private float rotationSpeed = 0.6f;
    [SerializeField]
    private float range = 10f;

    [SerializeField]
    private GameObject laserPrefab;

    // Start is called before the first frame update
    void Start()
    {
        var a = GameObject.FindGameObjectsWithTag("Player");
        player = a[0];

        LineRenderer lr = tip.GetComponent<LineRenderer>();
        lr.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsTarget(player.transform);

        if(Input.GetKeyDown(KeyCode.P))
        {
            Instantiate(laserPrefab, this.transform.position, this.transform.rotation);
        }

    }

    void Fire()
    {

    }


    void RotateTowardsTarget(Transform target)
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = target.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = rotationSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
