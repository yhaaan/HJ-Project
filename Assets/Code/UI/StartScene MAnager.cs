using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    void Awake()
    {
        // 1980x1080 해상도로 설정하고, 전체 화면 모드를 false로 설정합니다.
        Screen.SetResolution(1366, 768, false);
    }

    public void LoadGameScene()
    {
         
         SceneManager.LoadScene("GameScene"); 
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
