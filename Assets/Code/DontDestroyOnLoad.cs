using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    public static DontDestroyOnLoad instance;

    public int gameMode = 0;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}