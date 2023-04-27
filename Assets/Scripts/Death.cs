using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public delegate void LoseAction();
    public static event LoseAction OnDeath;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6 && OnDeath != null)
            OnDeath();

    }
}
