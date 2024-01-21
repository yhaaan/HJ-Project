using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSkill2 : MonoBehaviour
{
    private Player player;
    public float maxChargeTime = 1.0f;
    public float power = 0.0f;
    private float chargeTime;
    private bool isCharging;
    
    
    private void Start()
    {
        player = GetComponentInParent<Player>();
    }
    private void Update()
    {
        player.powerText.text = power.ToString("N0");
        if (Input.GetMouseButtonDown(0) && player.jumpCount > 0)
        {
            isCharging = true;
            chargeTime = 0;
        }

        if (isCharging)
        {
            if (chargeTime < maxChargeTime)
            {
                chargeTime += Time.deltaTime;
                power = Mathf.FloorToInt((chargeTime / maxChargeTime)* 100 ) ;
                player.powerText.text = power.ToString("N0");
            }
            if (Input.GetMouseButtonUp(0))
            {
                isCharging = false;
                player.Jump(-1*Mathf.Clamp(chargeTime / maxChargeTime, 0.1f, 1f));
                power = 0;
            }
        }

        
    }

    private void OnGUI()
    {
        if (isCharging)
        {
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position + (Vector3)player.direction.normalized);
            float size = Mathf.Lerp(10, 50, chargeTime / maxChargeTime);
            GUI.DrawTexture(new Rect(screenPosition.x - size / 2, Screen.height - screenPosition.y - size / 2, size, size), Texture2D.whiteTexture);
        }
    }
}
