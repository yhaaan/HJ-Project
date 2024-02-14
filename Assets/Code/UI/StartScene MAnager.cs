using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    void Awake()
    {
        // 1980x1080 �ػ󵵷� �����ϰ�, ��ü ȭ�� ��带 false�� �����մϴ�.
        Screen.SetResolution(1980, 1080, false);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene"); 
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
