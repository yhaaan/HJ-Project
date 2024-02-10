using System.Collections;
using UnityEngine;

public class DisappearingBlock : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // 블록의 SpriteRenderer 컴포넌트
    private Collider2D collider2D; // 블록의 Collider2D 컴포넌트
    private bool isFading = false; // 페이딩 중인지 여부를 추적

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어와의 충돌 감지
        if (collision.gameObject.CompareTag("Player") && !isFading)
        {
            StartCoroutine(FadeOutAndDisappear());
        }
    }

    private IEnumerator FadeOutAndDisappear()
    {
        isFading = true;

        // 5초 동안 투명해지게 함
        float fadeDuration = 5f;
        float fadeSpeed = 1f / fadeDuration;
        float amount = 0;

        while (amount < 1f)
        {
            amount += fadeSpeed * Time.deltaTime;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f - amount);
            yield return null;
        }

        // 완전히 투명해진 후 SpriteRenderer와 Collider2D 비활성화
        spriteRenderer.enabled = false;
        collider2D.enabled = false;

        // 5초 대기
        yield return new WaitForSeconds(5f);

        // 다시 활성화 및 원래 상태로 복구
        spriteRenderer.enabled = true;
        collider2D.enabled = true;
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        isFading = false;
    }
}
