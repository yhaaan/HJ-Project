using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Tile_Moving : MonoBehaviour
{
    public enum MoveType
    {
        X_Move,
        Y_Move
    }

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
        }
    }
}
