using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public delegate void NotifyAction(string n, float h);
    public static event NotifyAction OnNotification;

    public string notificationMessage;
    public float holdTime = 1.5f;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 6 && OnNotification != null)
            OnNotification(notificationMessage, holdTime);

    }

    public static void Nofify(string msg, float hld)
    {
        if (OnNotification != null)
            OnNotification(msg, hld);
    }
}
