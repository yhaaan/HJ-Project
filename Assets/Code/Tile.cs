using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    public enum BlockTypes
    {
        Common = 0,
        Ice,
        Disappear,
        GumWall,
        JumpingHigh
        
    }

    public BlockTypes blockType;
    public Sprite[] blockSprites;
    public PhysicsMaterial2D[] blockMaterials;
    private Collider2D coll;
    private SpriteRenderer myImage;
    private void Awake()
    {
        int ran = Random.Range(0, blockSprites.Length);
        myImage = GetComponent<SpriteRenderer>();
        myImage.sprite = blockSprites[ran];
        coll = GetComponent<Collider2D>();
        ChangeType();

    }



    void ChangeType()
    {
        switch (blockType)
        {
            case BlockTypes.Common:
                coll.sharedMaterial = blockMaterials[0];
                break;
            case BlockTypes.Ice:
                coll.sharedMaterial = blockMaterials[1];
                break;
            case BlockTypes.Disappear:
                break;
            case BlockTypes.GumWall:
                break;
            case BlockTypes.JumpingHigh:
                break;
        }
    }
}
