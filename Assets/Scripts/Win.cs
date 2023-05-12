using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public delegate void WinAction();
    public static event WinAction OnLevelFinish;

    bool hasTriggered;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6 && OnLevelFinish != null && !hasTriggered)
        {
            hasTriggered = true;
            OnLevelFinish();
        }
    }

    public static void TriggerWin()
    {
        if (OnLevelFinish != null)
            OnLevelFinish();
    }
}
