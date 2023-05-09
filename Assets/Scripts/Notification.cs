using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public delegate void NotifyAction(string n);
    public static event NotifyAction OnNotification;

    public string notificationMessage;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 6 && OnNotification != null)
            OnNotification(notificationMessage);

    }

    public static void Nofify(string msg)
    {
        if (OnNotification != null)
            OnNotification(msg);
    }
}
