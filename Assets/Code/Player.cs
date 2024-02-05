using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    public Text powerText;
    public GameObject[] jumpSkills;
    public float jumpForce = 10.0f;
    public int maxJumpCount = 1;
    public int jumpCount;
    public Vector2 direction;
    private Vector2 mousePosition;
    private Rigidbody2D playerRigidbody;
    

    // SpriteRenderer 변수 추가
    private SpriteRenderer spriteRenderer;

    // 점프 중임을 나타내는 변수를 추가
    private bool isJumping = false;

    // Animator 변수 추가
    public Animator animator;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer= GetComponent<SpriteRenderer>();

        // Animator 초기화
        animator = GetComponent<Animator>();


    }

    private void Update()
    {
        
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - (Vector2)transform.position;
        animator.SetFloat("Speed",playerRigidbody.velocity.y);
        animator.SetBool("isJumping",isJumping);
        // 점프 중이 아닐 때만 마우스 방향을 바라보게 수정
        if (!isJumping)
        {
            if (mousePosition.x < transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true; // 마우스 커서가 캐릭터의 오른쪽에 있을 때
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (transform.position.y > other.transform.position.y)
        {
            if (other.collider.CompareTag("Block"))
            {
                if (jumpCount < maxJumpCount)
                {
                    jumpCount = maxJumpCount;
                    isJumping = false;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        jumpCount--;
    }

    public void Jump(float power)
    {
        if (jumpCount > 0)
        {
            playerRigidbody.AddForce(direction.normalized * (jumpForce * power), ForceMode2D.Impulse);

            // 점프를 시작하면 isJumping을 true로 설정
            isJumping = true;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Jump);
        }
    }


    public void TESTZONE()
    {
        gameObject.transform.position = new Vector3(3, -4, 0);
    }
    public void TESTTelpo()
    {
        gameObject.transform.position = transform.position + new Vector3(0,10,0);
    }
}
