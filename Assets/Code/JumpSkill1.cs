using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSkill1 : MonoBehaviour
{
    private Player player;
    public float distance;
    public float maxDistance = 5.0f;
    public float power;
    
    private void Start()
    {
        player = GetComponentInParent<Player>();
    }
    private void Update()
    {
        distance = player.direction.magnitude;
        if (distance > maxDistance) distance = maxDistance;
        power = (distance / maxDistance)*100;
        player.powerText.text = power.ToString("N0");

        if (Input.GetMouseButtonDown(0))
        {
            player.Jump(Mathf.Clamp(distance / maxDistance, 0.1f, 1f));
        }
    }
    
}
