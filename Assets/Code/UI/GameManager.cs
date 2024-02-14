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
    private TMP_Text timerButtonText;
    public TMP_Text heightText;
    public Image heightButton;
    private TMP_Text heightButtonText;
    [Header("Setting")]
    public bool lookOutStatus = true;
    public Image lookOutButton;
    private TMP_Text lookOutButtonText;
    private void Awake()
    {
        // 1980x1080 해상도로 설정하고, 전체 화면 모드를 false로 설정합니다.
        Screen.SetResolution(1980, 1080, false);

        timerButtonText = timerButton.GetComponentInChildren<TMP_Text>();
        heightButtonText = heightButton.GetComponentInChildren<TMP_Text>();
        lookOutButtonText = lookOutButton.GetComponentInChildren<TMP_Text>();
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
            timerButtonText.text = "OFF";
            timerButton.color = Color.gray;
        }
        else
        {
            timerText.alpha = 1f;
            timerButtonText.text = "ON";
            timerButton.color = Color.white;
        }

    }

    public void HeightSwitch()
    {
        
        if (heightText.alpha == 1f)
        {
            heightText.alpha = 0f;
            heightButtonText.text = "OFF";
            heightButton.color = Color.gray;
        }
        else
        {
            heightText.alpha = 1f;
            heightButtonText.text = "ON";
            heightButton.color = Color.white;
        }
    }
    public void LookOutSwitch()
    {
        if (lookOutStatus)
        {
            lookOutStatus = false;
            lookOutButtonText.text = "OFF";
            lookOutButton.color = Color.gray;

        }
        else
        {
            lookOutStatus = true;
            lookOutButtonText.text = "ON";
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
