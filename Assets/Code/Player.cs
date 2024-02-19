using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
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
    public GameObject cutHam;
    //점프 블록 위에 있음


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

    private void OnEnable()
    {
        cutHam.SetActive(false);
        Camera.main.GetComponent<PixelPerfectCamera>().enabled = true;
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
        if (Input.GetKeyDown(KeyCode.T))
            TEST_TP();
        if (Input.GetKeyDown(KeyCode.R))
            TEST_TP2();
        if (!GameManager.instance.isPlaying)
            return;

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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Spring"))
        {
            AudioManager.instance.ChangeBgm(0);
            AudioManager.instance.PlayBgm(true);
        }
        else if (other.transform.CompareTag("Summer"))
        {
            AudioManager.instance.ChangeBgm(1);
            AudioManager.instance.PlayBgm(true);
        }
        else if (other.transform.CompareTag("Fall"))
        {
            AudioManager.instance.ChangeBgm(2);
            AudioManager.instance.PlayBgm(true);
        }
        else if (other.transform.CompareTag("Winter"))
        {
            AudioManager.instance.ChangeBgm(3);
            AudioManager.instance.PlayBgm(true);
        }
        
    }


    public void TEST_TP()
    {
        if (GameManager.instance.TEST == 0)
            return;
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 
            Input.mousePosition.y, -Camera.main.transform.position.z));
        gameObject.transform.position = point;
        
    }

    
    
    //private int test = 0;
    public void TEST_TP2()
    {
        /*
        if (test == 0)
        {
            gameObject.transform.position = new Vector3(16, 92, 0);
        }
        else if (test == 1)
        {
            gameObject.transform.position = new Vector3(16, 152, 0);
        }
        else if (test == 2)
        {
            gameObject.transform.position = new Vector3(16, 305, 0);
        }
        else if (test == 3)
        {
            gameObject.transform.position = new Vector3(16, 359, 0);
        }
        else if (test == 4)
        {
            gameObject.transform.position = new Vector3(16, 405, 0);
        }
        else if (test == 5)
        {
            gameObject.transform.position = new Vector3(16, 503, 0);
            test = -1;
        }

        test++;
        */
        
    }
    
}
