using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    public Sprite[] images;
    private SpriteRenderer myImage;
    private void Awake()
    {
        int ran = Random.Range(0, images.Length);
        myImage = GetComponent<SpriteRenderer>();
        myImage.sprite = images[ran];
    }
    
    
    
}
