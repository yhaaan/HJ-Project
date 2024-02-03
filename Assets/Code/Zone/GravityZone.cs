using System.Collections.Generic;
using UnityEngine;

public class GravityZone : MonoBehaviour
{
    public float gravityScaleInZone = 0.1f; // 영역 안에서의 중력 스케일

    private Dictionary<Rigidbody2D, float> originalGravityScales = new Dictionary<Rigidbody2D, float>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb != null) // 충돌한 오브젝트에 Rigidbody2D가 있는지 확인
        {
            // Store the original gravity scale
            originalGravityScales[rb] = rb.gravityScale;

            // Adjust gravity scale
            rb.gravityScale = gravityScaleInZone;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb != null) // 충돌한 오브젝트에 Rigidbody2D가 있는지 확인
        {
            // Restore the original gravity scale
            if (originalGravityScales.ContainsKey(rb))
            {
                rb.gravityScale = originalGravityScales[rb];
                originalGravityScales.Remove(rb); // Remove from the dictionary
            }
        }
    }
}
