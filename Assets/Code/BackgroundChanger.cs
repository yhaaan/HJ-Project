using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    public Transform player;
    public GameObject[] backgrounds; // 배경 오브젝트 배열
    public float[] heights; // 각 배경에 해당하는 높이 임계값 배열
    private int currentBackgroundIndex = -1; // 현재 활성화된 배경 인덱스 초기화

    private void Update()
    {
        CheckHeightAndChangeBackground();
    }

    void CheckHeightAndChangeBackground()
    {
        for (int i = 0; i < heights.Length; i++)
        {
            // 플레이어의 높이가 해당 임계값 범위 내에 있는지 확인
            if (player.position.y >= heights[i] && (i == heights.Length - 1 || player.position.y < heights[i + 1]))
            {
                // 현재 활성화된 배경이 아니라면 배경 변경
                if (currentBackgroundIndex != i)
                {
                    ActivateBackground(i);
                    currentBackgroundIndex = i;
                }
                break; // 적절한 배경을 찾았으므로 루프 종료
            }
        }
    }

    void ActivateBackground(int index)
    {
        // 모든 배경을 비활성화
        foreach (var bg in backgrounds)
        {
            bg.SetActive(false);
        }

        // 지정된 인덱스의 배경만 활성화
        backgrounds[index].SetActive(true);
    }
}
