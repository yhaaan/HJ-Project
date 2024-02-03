using UnityEngine;

public class WaterEffect : MonoBehaviour
{
    public float waterDrag = 2f; // 물 속에서의 드래그 값
    private float originalDrag; // 원래 드래그 값 저장용
    private bool isUnderwater = false; // 물 속 상태 확인

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                originalDrag = rb.drag; // 원래 드래그 값을 저장
                rb.drag = waterDrag; // 물 속 드래그로 변경
                isUnderwater = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null && isUnderwater)
            {
                rb.drag = originalDrag; // 원래 드래그 값으로 복원
                isUnderwater = false;
            }
        }
    }
}
