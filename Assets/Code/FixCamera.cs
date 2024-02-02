using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixCamera : MonoBehaviour
{
    public Player player;
    public float fixedXValue = 16.0f;
    public float dy = 2.0f;
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currentPosition = player.transform.position;

        // Fix the x-value
        currentPosition.x = fixedXValue;
        currentPosition.y = player.transform.position.y+dy;


        // Update the camera position
        transform.position = currentPosition;
    }


    public void UpView(float y)
    {
        dy += y;
    }
    
    public void DownView(float y)
    {
        dy -= y;
    }
}
