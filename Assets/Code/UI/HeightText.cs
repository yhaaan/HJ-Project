using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HeightText : MonoBehaviour
{
    private TMP_Text heightText;
    private void Awake()
    {
        heightText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        heightText.text = Player.instance.transform.position.y.ToString("N0");
    }
}
