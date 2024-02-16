using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour
{
    private string time;
    private TMP_Text timerText;
    private int h;
    private int m;
    private float s;

    private void Awake()
    {
        timerText = GetComponent<TMP_Text>();
        h = 0;
        m = 0;
        s = 0;
    }
    void Update()
    {
        if (!GameManager.instance.isPlaying)
            return;
        s += Time.deltaTime;
        if (s >= 60)
        {
            m++;
            s = 0;
        }
        if (m >= 60)
        {
            h++;
            m = 0;
        }

        time = h.ToString("N0") + "h\n" + m.ToString("N0") + "m\n" + s.ToString("F1") + "s";
        timerText.text = time;

    }

    public void ResetTime()
    {
        h = 0;
        m = 0;
        s = 0;
    }
}
