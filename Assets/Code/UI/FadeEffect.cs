using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeEffect : MonoBehaviour
{

    public static FadeEffect instance;
    [SerializeField] 
    [Range(0.01f, 10f)] 
    private float fadeTime;
    private Image myImage;
    [SerializeField] private AnimationCurve fadeCurve;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
        myImage = GetComponent<Image>();
    }

    public void RunFade(Color c)
    {
        gameObject.SetActive(true);
        StartCoroutine(Fade(1,0,c));
    }
    private IEnumerator Fade(float start,float end, Color c)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        while (percent<1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;
            myImage.color = c;
            Color color = myImage.color;
            //color.a = Mathf.Lerp(start, end, percent);
            color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            myImage.color = color;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
