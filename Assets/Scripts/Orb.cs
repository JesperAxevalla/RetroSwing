using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 6)
        {
            MasterScript.orbsCollected++;
            Debug.Log("Collected " + MasterScript.orbsCollected + "/" + MasterScript.orbs);
            if (MasterScript.orbsCollected == MasterScript.orbs)
                Win.TriggerWin();
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        
        MasterScript.orbs++;
        Debug.Log(MasterScript.orbs);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
