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
        player = FindObjectOfType<Player>(); // Player 타입의 컴포넌트를 자동으로 찾습니다.
    }

    void Update()
    {
        heightText.text = player.transform.position.y.ToString("N0");
    }
}
