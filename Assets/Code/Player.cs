using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text powerText;
    public GameObject[] jumpSkills;
    public float jumpForce = 10.0f;
    public int maxJumpCount = 1;
    public int jumpCount;
    public Vector2 direction;
    private Vector2 mousePosition;
    private Rigidbody2D playerRigidbody;

    // 스프라이트 변수 추가
    public Sprite jumpSprite;
    public Sprite fallSprite;
    public Sprite chargingSprite;

    // SpriteRenderer 변수 추가
    private SpriteRenderer spriteRenderer;

    // 점프 중임을 나타내는 변수를 추가
    private bool isJumping = false;

    // Animator 변수 추가
    private Animator animator;

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

        // 마우스 버튼이 눌려 있을 때
        if (Input.GetMouseButton(0))
        {
            // 애니메이터 비활성화 및 스프라이트 교체
            animator.enabled = false;
            spriteRenderer.sprite = chargingSprite;
        }
        // 마우스 버튼이 떼어졌을 때
        else if (Input.GetMouseButtonUp(0))
        {
            // 점프를 시작하면 isJumping을 true로 설정
            isJumping = true;
            // 점프 스프라이트로 교체
            spriteRenderer.sprite = jumpSprite;
        }
        else
        {
            // 점프 상태에 따라 애니메이터와 SpriteRenderer의 상태 변경
            if (isJumping)
            {
                // 점프 중일 때 애니메이터 비활성화
                animator.enabled = false;

                if (playerRigidbody.velocity.y > 0) // 점프 중
                {
                    spriteRenderer.sprite = jumpSprite;
                }
                else if (playerRigidbody.velocity.y < 0) // 낙하 중
                {
                    spriteRenderer.sprite = fallSprite;
                }
            }
            else
            {
                // 점프가 끝났을 때 애니메이터 활성화
                animator.enabled = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (transform.position.y > other.transform.position.y)
        {
            if (other.collider.CompareTag("Ground"))
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
        }
    }
}
