using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public static GameMode instance;

    public int game = 0;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
