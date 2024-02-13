using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    public float jumpForce = 10.0f;
    public int maxJumpCount = 1;
    public int jumpCount;
    public Vector2 direction;
    private Vector2 mousePosition;
    private Rigidbody2D playerRigidbody;
    public float originalJumpForce;

    //점프 블록 위에 있음
    public bool onJumpBlock = false;


    // SpriteRenderer 변수 추가
    private SpriteRenderer spriteRenderer;

    // 점프 중임을 나타내는 변수를 추가
    private bool isJumping = false;

    // Animator 변수 추가
    public Animator animator;

    private void Awake()
    {
        instance = this;
        originalJumpForce = jumpForce;

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
        if (Input.GetKeyDown(KeyCode.Space))
            TEST_TP();
        if (!GameManager.instance.isPlaying)
            return;
        if (onJumpBlock)
        {
            // 시간에 따라 색상을 무지개색으로 변경
            float colorValue = Mathf.PingPong(Time.time, 1f); // 0과 1 사이를 왕복
            spriteRenderer.color = Color.HSVToRGB(colorValue, 0.3f, 1f);
        }
        else
        {
            // 점프 블록을 떠나면 원래 색상으로 복구
            spriteRenderer.color = Color.white;
        }

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


    public void EnableSticky(bool isSticky)
{
    if (isSticky)
    {
        // 점착 상태를 활성화하는 로직
        // 예: Rigidbody2D의 velocity를 0으로 설정하여 움직임을 멈춤
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.GetComponent<Rigidbody2D>().isKinematic = true; // 물리적 영향을 받지 않도록 설정
    }
    else
    {
        // 점착 상태를 비활성화하는 로직
        // 예: Rigidbody2D의 isKinematic을 false로 설정하여 움직임을 다시 활성화
        this.GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
    
    public void TEST_TP()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 
            Input.mousePosition.y, -Camera.main.transform.position.z));
        gameObject.transform.position = point;
    }
}
