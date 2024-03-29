using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Tile_Moving : MonoBehaviour
{
    public enum MoveType
    {
        X_Move,
        Y_Move,
        Fog
    }
    
    private SpriteRenderer sprite;
    public MoveType type;
    [Header("Setting")] 
    public float distance;
    public float duration;

    private float oriX;
    private float oriY;
    private float desX;
    private float desY;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        
        oriX = transform.position.x;
        oriY = transform.position.y;
        desX = oriX + distance;
        desY = oriY + distance;
        StartMove();
    }

    private void StartMove()
    {
        switch (type)
        {
            case MoveType.X_Move:
                transform.DOMoveX(desX, duration).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);
                break;
            case MoveType.Y_Move:
                transform.DOMoveY(desY, duration).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);
                break;
            case MoveType.Fog:
                transform.DOMoveX(desX, duration).SetLoops(-1,LoopType.Restart).SetEase(Ease.Linear);
                break;
        }
    }

    private void Update()
    {
        if (type == MoveType.X_Move)
        {
            if (transform.position.x < oriX+0.5f)
                sprite.flipX = true;
            else if(transform.position.x > desX-0.5f)
                sprite.flipX = false;
        }
    }
}
