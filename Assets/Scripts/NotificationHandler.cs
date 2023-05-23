using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationHandler : MonoBehaviour
{
    public TextMeshProUGUI textGUI;

    public float fadeTime = 0.5f;

    private bool isActive;

    // Start is called before the first frame update
    private void OnEnable()
    {
        Notification.OnNotification += DisplayMessage;
    }

    private void OnDisable()
    {
        Notification.OnNotification -= DisplayMessage;
    }

    private IEnumerator FadeText(float holdTime)
    {
        isActive = true;

        float currentTime = 0f;
        while (currentTime < fadeTime)
        {
            float alpha = Mathf.Lerp(0f, 1f, currentTime / fadeTime);
            textGUI.color = new Color(textGUI.color.r, textGUI.color.g, textGUI.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }


        yield return new WaitForSeconds(holdTime);


        currentTime = 0f;
        while (currentTime < fadeTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / fadeTime);
            textGUI.color = new Color(textGUI.color.r, textGUI.color.g, textGUI.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        isActive = false;
        yield break;
    }

    void DisplayMessage(string msg, float holdTime)
    {
        textGUI.text = msg;

        if (!isActive)
            StartCoroutine(FadeText(holdTime));
    }

}
