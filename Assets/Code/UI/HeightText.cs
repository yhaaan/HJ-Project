using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HeightText : MonoBehaviour
{
    private Text heightText;
    private void Awake()
    {
        heightText = GetComponent<Text>();
    }

    void Update()
    {
        heightText.text = Player.instance.transform.position.y.ToString("N0");
    }
}
