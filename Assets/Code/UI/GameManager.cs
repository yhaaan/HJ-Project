using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int TEST = 0;
    public static GameManager instance;
    public bool isPlaying = true;
    private bool isFullScreen;
    public GameObject setting;
    public GameObject gameStages;
    public Player player;
    public GameObject cutScene;
    [Header("UI")] 
    public Text timerText;
    public Image timerButton;
    public Text heightText;
    public Image heightButton;

    [Header("Setting")] 
    public bool lookOutStatus = true;
    public Image lookOutButton;
    public Image fullScreenButton;
    public Text saveDataText;
    public Text loadDataText;
    public int loadCount;


    public void TESTC()
    {
        if(TEST == 0)
            TEST = 1;
        else
        {
            TEST = 0;
        }
    }
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
        // 1980x1080 해상도로 설정하고, 전체 화면 모드를 false로 설정합니다.
        Screen.SetResolution(960, 540, false);
        gameStages.SetActive(true);
        isPlaying = false;
        isFullScreen = false;
        GameStart();
    }

    public void GameStart()
    {
        
        loadCount = 0;
        isPlaying = true;
        setting.SetActive(false);
        player.transform.position = new Vector3(16, 0, 0);
        timerText.GetComponent<Timer>().ResetTime();
        SaveDataTextUpdate();
        LoadDataTextUpdate();
        
        FadeEffect.instance.RunFade(Color.black);
        if (GameMode.instance.game == 0)
        {
            cutScene.SetActive(true);
            Camera.main.GetComponent<PixelPerfectCamera>().enabled = false;
        }
        else if(GameMode.instance.game == 1)
            Load();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Click);
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
            saveDataText.text = score + "F\n" + h + "h " + m + "m " + s.ToString("F1") + "s";
        }
        else
        {
            saveDataText.text = "No SaveData";
        }
    }

    public void LoadDataTextUpdate()
    {
        if (PlayerPrefs.HasKey("LoadCheck"))
        {
            loadCount = PlayerPrefs.GetInt("LoadCheck");
            loadDataText.text = loadCount.ToString();
        }
        else
        {
            loadDataText.text = "★";
        }
    }

    public void TimerSwitch()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Click);
        if (timerText.color.a == 1f)
        {
            Color c = timerText.color;
            c.a = 0;
            timerText.color = c;
            //timerButtonText.text = "OFF";
            timerButton.color = Color.white;
            
        }
        else
        {
            Color c = timerText.color;
            c.a = 1;
            timerText.color = c;
            //timerButtonText.text = "ON";
            timerButton.color = Color.gray;
        }

    }

    public void HeightSwitch()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Click);
        if (heightText.color.a == 1f)
        {
            Color c = heightText.color;
            c.a = 0;
            heightText.color = c;
            //heightButtonText.text = "OFF";
            heightButton.color = Color.white;
            
        }
        else
        {
            Color c = heightText.color;
            c.a = 1;
            heightText.color = c;
            //heightButtonText.text = "ON";
            heightButton.color = Color.gray;
            
        }
    }

    public void LookOutSwitch()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Click);
        if (lookOutStatus)
        {
            lookOutStatus = false;
            //lookOutButtonText.text = "OFF";
            lookOutButton.color = Color.white;

        }
        else
        {
            lookOutStatus = true;
            //lookOutButtonText.text = "ON";
            lookOutButton.color = Color.gray;
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
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Click);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else

                        Application.Quit();
#endif
    }

    public void ReStartGame()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Click);
        Esc();
        PlayerPrefs.DeleteAll();
        GameMode.instance.game = 0;
        GameStart();
    }

    public void Title()
    {
        Esc();
        SceneManager.LoadScene("StartScene");
    }

    public void FullScreen()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Click);
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
        if(!PlayerPrefs.HasKey("SaveCheck"))
            return;
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
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Click);
        Esc();
        Load();
    }


    

}
