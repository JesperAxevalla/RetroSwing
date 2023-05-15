using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private GameObject player;

    [Header("References")]
    [SerializeField]
    private GameObject body;
    [SerializeField]
    private GameObject tip;
    [SerializeField]
    private GameObject laserPrefab;


    [Header("Settings")]
    [SerializeField]
    private float rotationSpeed = 0.6f;
    [SerializeField]
    private float range = 10f;

    private float lockedTimer = 0f;
    [SerializeField]
    private float lockedTrigger = 0.5f;
    [SerializeField]
    private float viewAngle = 4f;
    private bool locked = false;


    // Start is called before the first frame update
    void Start()
    {
        var a = GameObject.FindGameObjectsWithTag("Player");
        player = a[0];

        LineRenderer lr = tip.GetComponent<LineRenderer>();
        lr.enabled = true;

        LaserLengthUpdate();
    }

    bool PlayerInRange()
    {
        float dist = Vector3.Distance(player.transform.position, body.transform.position);
        return dist < range;
    }

    bool CanSeePlayer()
    {
        Vector3 targetDir = player.transform.position - body.transform.position;
        float angle = Vector3.Angle(targetDir, body.transform.forward);

        if (angle > viewAngle) return false;



        RaycastHit hit;

        int layerMask = 1 << 21;
        layerMask = ~21;

        Physics.Raycast(tip.transform.position, player.transform.position - tip.transform.position, out hit, range, layerMask);

        if (hit.transform == null) return false;
        if (hit.transform.gameObject.tag == "Player") return true;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        LaserLengthUpdate();

        if (!PlayerInRange()) return;

        RotateTowardsTarget(player.transform);

        if (CanSeePlayer())
            lockedTimer += Time.deltaTime;
        else
            lockedTimer -= Time.deltaTime;

        if (lockedTimer >= lockedTrigger)
        {
            Fire();
            lockedTimer = 0f;
        }
    }


    void LaserLengthUpdate()
    {
        lr_need_update = false;

        RaycastHit hit;
        
        Physics.Raycast(tip.transform.position, tip.transform.forward, out hit, range);

        if (hit.transform != null)
        {
            if(!hit.collider.isTrigger)
            {

                LineRenderer lr = tip.GetComponent<LineRenderer>();
                lr.SetPosition(1, new Vector3(0, 0, Vector3.Distance(tip.transform.position, hit.point)+0.5f));
            }
        }
    }

    void Fire()
    {
        Instantiate(laserPrefab, tip.transform.position + (tip.transform.forward * 3f), body.transform.rotation);
    }

    bool lr_need_update;

    void RotateTowardsTarget(Transform target)
    {
        lr_need_update = true;

        Vector3 targetDirection = target.position - body.transform.position;
        float singleStep = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(body.transform.forward, targetDirection, singleStep, 1.0f);
        body.transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
