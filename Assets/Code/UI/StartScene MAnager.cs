using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public GameObject load;
    public Image loadImage;
    public Button loadButton;
    void Awake()
    {
        loadButton = load.GetComponent<Button>();
        loadImage = load.GetComponent<Image>();
        // 1980x1080 해상도로 설정하고, 전체 화면 모드를 false로 설정합니다.
        Screen.SetResolution(960, 540, false);
        if (!PlayerPrefs.HasKey("SaveCheck"))
        {
            Color c = loadImage.color;
            c.a = 0.4f;
            loadImage.color = c;
            loadButton.interactable = false;
        }
    }

    public void StartButton()
    {
         GameMode.instance.game = 0;
         SceneManager.LoadScene("GameScene"); 
         //ChangeScene.instance.RunFade();
    }
    
    public void LoadButton()
    {
        if (PlayerPrefs.HasKey("SaveCheck"))
        {
            GameMode.instance.game = 1;
            SceneManager.LoadScene("GameScene");
        }
        //ChangeScene.instance.RunFade();
    }

    public void QuitGame()
    {
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        
        Application.Quit();
        #endif
    }
    
}
