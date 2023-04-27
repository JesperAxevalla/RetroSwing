using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public delegate void WinAction();
    public static event WinAction OnLevelFinish;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6 && OnLevelFinish != null)
            OnLevelFinish();

    }
}
