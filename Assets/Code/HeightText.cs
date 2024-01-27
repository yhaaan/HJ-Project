using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeightText : MonoBehaviour
{
    public Player player;
    private Text heightText;
    private void Awake()
    {
        heightText = GetComponent<Text>();
    }

    void Update()
    {
        heightText.text = player.transform.position.y.ToString("N0");
    }
}
