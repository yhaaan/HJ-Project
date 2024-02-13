using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Button = UnityEngine.UIElements.Button;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPlaying = true;
    
    public GameObject setting;

    [Header("UI")] 
    public TMP_Text timerText;
    public Image timerButton;
    public TMP_Text heightText;
    public Image heightButton;
    [Header("Setting")]
    public bool lookOutStatus = true;
    public TMP_Text lookOutText;
    public Image lookOutButton;

    private void Awake()
    {
        instance = this;
        isPlaying = true;
        setting.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPlaying)
            {
                isPlaying = false;
                setting.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                isPlaying = true;
                setting.SetActive(false);
                Time.timeScale = 1;

            }
        }
    }

    public void TimerSwitch()
    {
        if (timerText.alpha == 1f)
        {
            timerText.alpha = 0f;
            timerButton.color = Color.gray;
        }
        else
        {
            timerText.alpha = 1f;
            timerButton.color = Color.white;
        }

    }

    public void HeightSwitch()
    {
        
        if (heightText.alpha == 1f)
        {
            heightText.alpha = 0f;
            heightButton.color = Color.gray;
        }
        else
        {
            heightText.alpha = 1f;
            heightButton.color = Color.white;
        }
    }
    public void LookOutSwitch()
    {
        if (lookOutStatus)
        {
            lookOutStatus = false;
            lookOutText.text = "OFF";
            lookOutButton.color = Color.gray;

        }
        else
        {
            lookOutStatus = true;
            lookOutText.text = "ON";
            lookOutButton.color = Color.white;
        }
    }

    public void DragHeight()
    {
        if(isPlaying)
            return;
        heightText.transform.position =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
    }
    public void DragTimer()
    {
        if(isPlaying)
            return;
        timerText.transform.position =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
    }
}
