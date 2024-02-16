using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Saver : MonoBehaviour
{
   
    public enum SaveAndLoad
    {
        SAVE = 0,
        LOAD
    }

    public SaveAndLoad type;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            
            if (type == SaveAndLoad.SAVE)
            {
                GameManager.instance.Save();
            }else if (type == SaveAndLoad.LOAD)
            {
                GameManager.instance.Load();
            }
        }
    }


    
}
