using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jump : MonoBehaviour
{
    private Player player;
    public float maxChargeTime = 1.0f;
    public float power = 0.0f;
    private float chargeTime;
    private bool isCharging;
    public Image chargeIndicator; // 충전 인디케이터로 사용할 Image 컴포넌트

    private void Start()
    {
        player = GetComponentInParent<Player>();
        chargeIndicator.enabled = false;
    }

    private void Update()
    {
        player.powerText.text = power.ToString("N0");

        if (Input.GetMouseButtonDown(0) && player.jumpCount > 0)
        {
            isCharging = true;
            chargeTime = 0;
            chargeIndicator.enabled = true;
        }

        if (isCharging)
        {
            chargeTime += Time.deltaTime;
            if (chargeTime > maxChargeTime)
            {
                chargeTime = maxChargeTime;
            }

            power = Mathf.FloorToInt((chargeTime / maxChargeTime) * 100);
            player.powerText.text = power.ToString("N0");

            // 인디케이터 크기와 색상, 위치 변경
            UpdateChargeIndicatorSizeAndColor();
            UpdateChargeIndicatorPosition();
        }

        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            isCharging = false;
            player.Jump(-1 * Mathf.Clamp(chargeTime / maxChargeTime, 0.1f, 1f));
            power = 0;
            chargeIndicator.enabled = false;
        }
    }

    private void UpdateChargeIndicatorPosition()
    {
        Vector2 playerScreenPosition = Camera.main.WorldToScreenPoint(player.transform.position);
        Vector2 mousePosition = Input.mousePosition;
        Vector2 direction = (mousePosition - playerScreenPosition).normalized;
        float distance = 60f; // 원 인디케이터와 플레이어 사이의 거리
        Vector2 indicatorPosition = playerScreenPosition + direction * distance;

        chargeIndicator.transform.position = indicatorPosition;
    }

    private void UpdateChargeIndicatorSizeAndColor()
    {
        float size = Mathf.Lerp(10, 40, chargeTime / maxChargeTime);
        chargeIndicator.transform.localScale = new Vector3(size, size, 1);
        chargeIndicator.color = Color.Lerp(Color.green, Color.red, chargeTime / maxChargeTime);
    }

}
