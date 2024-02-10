using UnityEngine;

public class JumpBlock : MonoBehaviour
{
    public float enhancedJumpForce = 45f; // 이 블록이 플레이어에게 제공할 증가된 점프력

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어의 점프력을 증가시킨다.
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.jumpForce = enhancedJumpForce;
                player.onJumpBlock = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어가 블록을 벗어나면 점프력을 원래대로 복구한다.
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.jumpForce = player.originalJumpForce;
                player.onJumpBlock = false;
            }
        }
    }
}
