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

    public float lockedTimer = 0f;
    [SerializeField]
    private float lockedTrigger = 0.5f;
    [SerializeField]
    private float viewAngle = 4f;
    private bool locked = false;

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = tip.transform.position;
        originalPosition += tip.transform.forward * 2;

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

    bool PlayerLOS()
    {
        RaycastHit hit;

        int layerMask = 1 << 21;
        layerMask = ~21;

        Physics.Raycast(tip.transform.position, player.transform.position - tip.transform.position, out hit, range, layerMask);

        if (hit.transform == null) return false;
        if (hit.transform.gameObject.tag == "Player") return true;
        return false;
    }

    bool PlayerInViewcone()
    {
        Vector3 targetDir = player.transform.position - body.transform.position;
        float angle = Vector3.Angle(targetDir, body.transform.forward);

        if (angle > viewAngle) return false;
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (lr_need_update)
            LaserLengthUpdate();

        if (lockedTimer >= lockedTrigger)
            Fire();

        if (PlayerInRange())
        {
            if (PlayerLOS())
            {
                RotateTowardsTarget(player.transform.position);
                if (PlayerInViewcone())
                    lockedTimer += Time.deltaTime;
            }
            else
                CantSee();
        }
        else
            CantSee();

    }

    void CantSee()
    {
        if (lockedTimer > 0f)
            lockedTimer -= Time.deltaTime;
        else if (lockedTimer < 0f)
            lockedTimer = 0f;
        if (lockedTimer == 0f)
            RotateTowardsTarget(originalPosition);

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
        lockedTimer = 0f;
        Instantiate(laserPrefab, tip.transform.position + (tip.transform.forward * 3f), body.transform.rotation);
    }

    bool lr_need_update;

    void RotateTowardsTarget(Vector3 target)
    {
        lr_need_update = true;

        Vector3 targetDirection = target - body.transform.position;
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
