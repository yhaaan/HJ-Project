using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public Transform look;
    public float power;
    private Rigidbody2D playerRigid;
    private LineRenderer lineRenderer;
    public Vector2 jumpVec;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        playerRigid = player.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = player.position;
        jumpVec = mousePos - playerPos;
        lineRenderer.SetPosition(0,mousePos);
        lineRenderer.SetPosition(1,playerPos);
        
    }


    private void OnMouseDown()
    {
        Debug.Log("11");
        jumpVec.Normalize();
        playerRigid.AddForce(jumpVec * power , ForceMode2D.Impulse);

    }
}
