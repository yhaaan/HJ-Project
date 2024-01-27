using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveTile : MonoBehaviour
{
   
    private void Awake()
    {
        
       
    }

    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player") && transform.position.y <= 150)
        {
            transform.DOMoveY(150, 10);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            transform.DOMoveY(110, 10);
        }
    }
}
