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
    public GameObject setting;
    public GameObject gameStages;
    public Player player;
    [Header("UI")] 
    public TMP_Text timerText;
    public Image timerButton;
    public TMP_Text heightText;
    public Image heightButton;
    
    [Header("Setting")] 
    public bool lookOutStatus = true;
    public Image lookOutButton;
    public Image fullScreenButton;
    public TMP_Text saveDataText;
    public TMP_Text loadDataText;
    public int loadCount;


    private void Start()
    {
        // 1980x1080 해상도로 설정하고, 전체 화면 모드를 false로 설정합니다.
        Screen.SetResolution(960, 540, false);
        gameStages.SetActive(true);
        instance = this;
        isPlaying = false;
        isFullScreen = false;
        
        GameStart();
    }

    public void GameStart()
    {
        loadCount = 0;
        isPlaying = true;
        setting.SetActive(false);
        player.transform.position = new Vector3(4, 6, 0);
        timerText.GetComponent<Timer>().ResetTime();
        SaveDataTextUpdate();
        LoadDataTextUpdate();
        FadeEffect.instance.RunFade(Color.black);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Esc();
        }
    }

    public void Esc()
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

    public void SaveDataTextUpdate()
    {
        if (PlayerPrefs.HasKey("SaveCheck"))
        {
            int h = PlayerPrefs.GetInt("Time_h");
            int m = PlayerPrefs.GetInt("Time_m");
            float s = PlayerPrefs.GetFloat("Time_s");
            int score = PlayerPrefs.GetInt("Score");
            saveDataText.text = score + "층\n" + h + "h\n" + m + "m\n" + s.ToString("F1") + "s";
        }
        else
        {
            saveDataText.text = "없음";
        }
    }
    public void LoadDataTextUpdate()
    {
        if (PlayerPrefs.HasKey("LoadCheck"))
        {
            loadCount = PlayerPrefs.GetInt("LoadCheck");
            loadDataText.text = "파란나비의 도움 " + loadCount + " 번";
        }
        else
        {
            loadDataText.text = "파란나비의 도움은 필요없다";
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

    public void ReStartGame()
    {
        Esc();
        PlayerPrefs.DeleteAll();
        GameStart();
    }

    public void Title()
    {
        Esc();
        SceneManager.LoadScene("StartScene");
    }

    public void FullScreen()
    {
        if (isFullScreen)
        {
            isFullScreen = false;
            Screen.SetResolution(960, 540, false);
            fullScreenButton.color = Color.white;
        }
        else
        {
            isFullScreen = true;
            Screen.SetResolution(1920, 1080, true);
            fullScreenButton.color = Color.gray;
        }
    }
    
    public void Save()
    {
        Timer timer = timerText.GetComponent<Timer>();
        PlayerPrefs.SetInt("SaveCheck", 1);
        PlayerPrefs.SetFloat("PlayerX", Player.instance.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", Player.instance.transform.position.y);
        PlayerPrefs.SetInt("Time_h", timer.h);
        PlayerPrefs.SetInt("Time_m", timer.m);
        PlayerPrefs.SetFloat("Time_s", timer.s);
        PlayerPrefs.SetInt("Score", (int)Player.instance.transform.position.y);
        SaveDataTextUpdate();
    }

    public void Load()
    {
        loadCount++;
        PlayerPrefs.SetInt("LoadCheck", loadCount);
        //Fade 색
        FadeEffect.instance.RunFade(Color.white);
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        Player.instance.transform.position = new Vector3(x, y, 0);
        Player.instance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        //시간 로드
        int h = PlayerPrefs.GetInt("Time_h");
        int m = PlayerPrefs.GetInt("Time_m");
        float s = PlayerPrefs.GetFloat("Time_s");
        timerText.GetComponent<Timer>().SetTime(h, m, s);
        LoadDataTextUpdate();
        
    }

    public void LoadButton()
    {
        Esc();
        Load();
    }
}
