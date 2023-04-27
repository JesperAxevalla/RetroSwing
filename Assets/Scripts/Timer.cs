using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static double current, last, total = 0;

    public TextMeshProUGUI textCurrent, textLast, textTotal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inc = Time.deltaTime;

        current += inc;
        total += inc;

        var cTimespan = System.TimeSpan.FromSeconds(current);
        var lTimespan = System.TimeSpan.FromSeconds(last);
        var tTimespan = System.TimeSpan.FromSeconds(total);

        textLast.text = lTimespan.Minutes.ToString("D2") + ":" + lTimespan.Seconds.ToString("D2");
        textCurrent.text = cTimespan.Minutes.ToString("D2") + ":" + cTimespan.Seconds.ToString("D2");
        textTotal.text = tTimespan.Minutes.ToString("D2") + ":" + tTimespan.Seconds.ToString("D2");
    }

    public static void NextLevel()
    {
        last = current;
        total = Mathf.Floor((float)total);
        current = 0;
    }


}
