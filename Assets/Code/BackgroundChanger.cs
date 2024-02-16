using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    public Transform player;
    public GameObject[] backgrounds; // ��� ������Ʈ �迭
    public float[] heights; // �� ��濡 �ش��ϴ� ���� �Ӱ谪 �迭
    private int currentBackgroundIndex = -1; // ���� Ȱ��ȭ�� ��� �ε��� �ʱ�ȭ

    private void Update()
    {
        CheckHeightAndChangeBackground();
    }

    void CheckHeightAndChangeBackground()
    {
        for (int i = 0; i < heights.Length; i++)
        {
            // �÷��̾��� ���̰� �ش� �Ӱ谪 ���� ���� �ִ��� Ȯ��
            if (player.position.y >= heights[i] && (i == heights.Length - 1 || player.position.y < heights[i + 1]))
            {
                // ���� Ȱ��ȭ�� ����� �ƴ϶�� ��� ����
                if (currentBackgroundIndex != i)
                {
                    ActivateBackground(i);
                    currentBackgroundIndex = i;
                }
                break; // ������ ����� ã�����Ƿ� ���� ����
            }
        }
    }

    void ActivateBackground(int index)
    {
        // ��� ����� ��Ȱ��ȭ
        foreach (var bg in backgrounds)
        {
            bg.SetActive(false);
        }

        // ������ �ε����� ��游 Ȱ��ȭ
        backgrounds[index].SetActive(true);
    }
}
