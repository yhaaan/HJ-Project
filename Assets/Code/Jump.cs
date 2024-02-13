using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jump : MonoBehaviour
{
    public float maxChargeTime = 1.0f;
    public float power = 0.0f;
    private float chargeTime;
    private bool isCharging;
    public Image chargeIndicator; // 충전 인디케이터로 사용할 Image 컴포넌트

    private void Start()
    {
        chargeIndicator.enabled = false;
    }

    private void Update()
    {
        if (!GameManager.instance.isPlaying)
            return;
        Player.instance.animator.SetBool("isCharging",isCharging);
        

        if (Input.GetMouseButtonDown(0) && Player.instance.jumpCount > 0)
        {
            isCharging = true;
            chargeTime = 0;
            chargeIndicator.enabled = true;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.StartCharging);
        }

        if (isCharging)
        {
            
            chargeTime += Time.deltaTime;
            if (chargeTime > maxChargeTime)
            {
                chargeTime = maxChargeTime;
                //AudioManager.instance.PlaySfx(AudioManager.Sfx.FullCharging);
                //풀자징시 효과음은 없는게 낫다고 판단
            }

            power = Mathf.FloorToInt((chargeTime / maxChargeTime) * 100);

            // 인디케이터 크기와 색상, 위치 변경
            UpdateChargeIndicatorSizeAndColor();
            UpdateChargeIndicatorPosition();
        }

        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            isCharging = false;
            Player.instance.Jump(-1 * Mathf.Clamp(chargeTime / maxChargeTime, 0.1f, 1f));
            power = 0;
            chargeIndicator.enabled = false;
        }
        
        //차징 취소
        if (Input.GetMouseButtonDown(1) && isCharging)
        {
            isCharging = false;
            power = 0;
            chargeIndicator.enabled = false;
        }
    }

    private void UpdateChargeIndicatorPosition()
    {
        Vector2 playerScreenPosition = Camera.main.WorldToScreenPoint(Player.instance.transform.position);
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
