using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPlaying = true;
    private bool isFullScreen;
    private bool isHalfFhd;
    public GameObject setting;

    [Header("UI")] public TMP_Text timerText;
    public Image timerButton;
    public TMP_Text heightText;
    public Image heightButton;
    [Header("Setting")] 
    public bool lookOutStatus = true;
    public Image lookOutButton;
    public Image fullScreenButton;
    public Image halfFhdScreenButton;


    private void Awake()
    {
        // 1980x1080 해상도로 설정하고, 전체 화면 모드를 false로 설정합니다.
        Screen.SetResolution(1366, 768, false);
        
        instance = this;
        isPlaying = true;
        isFullScreen = false;
        isHalfFhd = false;
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
            //timerButtonText.text = "OFF";
            timerButton.color = Color.gray;
        }
        else
        {
            timerText.alpha = 1f;
            //timerButtonText.text = "ON";
            timerButton.color = Color.white;
        }

    }

    public void HeightSwitch()
    {

        if (heightText.alpha == 1f)
        {
            heightText.alpha = 0f;
            //heightButtonText.text = "OFF";
            heightButton.color = Color.gray;
        }
        else
        {
            heightText.alpha = 1f;
            //heightButtonText.text = "ON";
            heightButton.color = Color.white;
        }
    }

    public void LookOutSwitch()
    {
        if (lookOutStatus)
        {
            lookOutStatus = false;
            //lookOutButtonText.text = "OFF";
            lookOutButton.color = Color.gray;

        }
        else
        {
            lookOutStatus = true;
            //lookOutButtonText.text = "ON";
            lookOutButton.color = Color.white;
        }
    }

    public void DragHeight()
    {
        if (isPlaying)
            return;
        heightText.transform.position =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
    }

    public void DragTimer()
    {
        if (isPlaying)
            return;
        timerText.transform.position =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else

                        Application.Quit();
        #endif
    }

    public void LoadTitleScene()
    {
        isPlaying = true;
        SceneManager.LoadScene("StartScene");
    }

    public void FullScreen()
    {
        if (isFullScreen)
        {
            isFullScreen = false;
            if(isHalfFhd)
                Screen.SetResolution(960, 540, false);
            else
                Screen.SetResolution(1366, 768, false);
            fullScreenButton.color = Color.white;
        }
        else
        {
            isFullScreen = true;
            Screen.SetResolution(1920, 1080, true);
            fullScreenButton.color = Color.gray;
        }
    }
    public void HalfFHD()
    {

        if (isHalfFhd)
        {
            Screen.SetResolution(1366, 768, false);
            isHalfFhd = false;
            halfFhdScreenButton.color = Color.white;
        }
        else
        {
            Screen.SetResolution(960, 540, false);
            isHalfFhd = true;
            halfFhdScreenButton.color = Color.gray;
        }


    }
}
