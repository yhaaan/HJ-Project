/*using UnityEngine;

public class StickyBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어와 점착 블록이 충돌했을 때 호출
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어의 isSticky 플래그를 true로 설정하여 점착 상태 활성화
            var player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.SetSticky(true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 플레이어가 점착 블록을 떠났을 때 호출
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어의 isSticky 플래그를 false로 설정하여 점착 상태 비활성화
            var player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.SetSticky(false);
            }
        }
    }
}
*/