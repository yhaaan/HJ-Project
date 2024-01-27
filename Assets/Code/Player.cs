using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text powerText;
    public int TESTJUMPMODE = 1;
    public GameObject[] jumpSkills;
    public float jumpForce = 10.0f;
    public int maxJumpCount = 1;
    public int jumpCount;
    public Vector2 direction;
    private Vector2 mousePosition;
    private Rigidbody2D playerRigidbody;
    
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        JumpModeChange(TESTJUMPMODE);
    }
    private void Update()
    {
        Camera.main.transform.position = new Vector3(18, transform.position.y+2, -10);
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - (Vector2)transform.position;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (transform.position.y > other.transform.position.y)
        {
            if (other.collider.CompareTag("Ground"))
            {
                if (jumpCount < maxJumpCount)
                    jumpCount = maxJumpCount;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        jumpCount--;
    }


    public void Jump(float power){
        if (jumpCount > 0)
        {
            playerRigidbody.AddForce(direction.normalized * (jumpForce * power), ForceMode2D.Impulse);
            
        }
    }

    public void JumpModeChange(int modeNum)
    {
        foreach (var jumpSkill in jumpSkills) 
        { 
            if (jumpSkill == jumpSkills[TESTJUMPMODE]) 
                jumpSkill.SetActive(true);
            else jumpSkill.SetActive(false);
        }
            
    }

}