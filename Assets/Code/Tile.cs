using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    public enum BlockTypes
    {
        Common = 0,
        Bounce,
        Ice,
        Disappear,
        GumWall,
        PowerJump
    }
   
    public BlockTypes blockType;
    public Sprite[] blockSprites;
    public PhysicsMaterial2D[] blockMaterials;
    private Collider2D coll;
    private SpriteRenderer myImage;
    private bool isFading = false;
    private void Awake()
    {
        
        int ran = Random.Range(0, blockSprites.Length);
        myImage = GetComponent<SpriteRenderer>();
        myImage.sprite = blockSprites[ran];
        coll = GetComponent<Collider2D>();
        ChangeType();
        int filpRan = Random.Range(0, 2);
        if (filpRan == 1) myImage.flipX = true;
        else myImage.flipX = false;

    }



    private void ChangeType()
    {
        switch (blockType)
        {
            case BlockTypes.Common:
                coll.sharedMaterial = blockMaterials[0];
                break;
            case BlockTypes.Bounce:
                coll.sharedMaterial = blockMaterials[1];
                break;
            case BlockTypes.Ice:
                coll.sharedMaterial = blockMaterials[2];
                break;
            case BlockTypes.Disappear:
                coll.sharedMaterial = blockMaterials[0];
                break;
            case BlockTypes.GumWall:
                coll.sharedMaterial = blockMaterials[0];
                break;
            case BlockTypes.PowerJump:
                coll.sharedMaterial = blockMaterials[0];
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            switch (blockType)
            {
                case BlockTypes.Disappear:
                    if (!isFading) StartCoroutine(FadeOutAndDisappear());
                    break;
                case BlockTypes.PowerJump:
                    Player.instance.jumpForce = 45f;
                    break;
                
            }
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            switch (blockType)
            {
                case BlockTypes.PowerJump:
                    Player.instance.jumpForce = 30f;
                    break;
            }
        }
    }
    
    private IEnumerator FadeOutAndDisappear()
    {
        isFading = true;

        // 5초 동안 투명해지게 함
        float fadeDuration = 1.7f;
        float fadeSpeed = 1f / fadeDuration;
        float amount = 0;

        while (amount < 1f)
        {
            amount += fadeSpeed * Time.deltaTime;
            myImage.color = new Color(1f, 1f, 1f, 1f - amount);
            yield return null;
        }

        // 완전히 투명해진 후 SpriteRenderer와 Collider2D 비활성화
        myImage.enabled = false;
        coll.enabled = false;

        // 5초 대기
        yield return new WaitForSeconds(3f);

        // 다시 활성화 및 원래 상태로 복구
        myImage.enabled = true;
        coll.enabled = true;
        myImage.color = new Color(1f, 1f, 1f, 1f);
        isFading = false;
    }
}
