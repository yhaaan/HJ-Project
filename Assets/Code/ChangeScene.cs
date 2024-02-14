using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene instance;
    [SerializeField] [Range(0.01f, 10f)] 
    private float fadeTime;
    private Image myImage;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
        myImage = GetComponent<Image>();
    }

    public void RunFade()
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeInOut());
    }
    public IEnumerator FadeInOut()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(1, 0));
            //yield return StartCoroutine(Fade(1, 0));
            break;
        }
        gameObject.SetActive(false);
    }

    private IEnumerator Fade(float start,float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        while (percent<1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = myImage.color;
            color.a = Mathf.Lerp(start, end, percent);
            myImage.color = color;
            yield return null;
        }
    }
}
