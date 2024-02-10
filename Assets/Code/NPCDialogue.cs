using UnityEngine;
using UnityEngine.UI; // UI 사용을 위해 추가
using System.Collections; // 코루틴 사용을 위해 추가

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialogueTextObject; // 대화 텍스트 오브젝트
    public string dialogueText = "안녕하세요! NPC입니다."; // 표시할 대사
    private Text dialogueTextComponent; // 대사를 표시할 Text 컴포넌트

    private void Start()
    {
        dialogueTextComponent = dialogueTextObject.GetComponent<Text>();
        dialogueTextObject.SetActive(false); // 초기에는 대사를 숨김
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 플레이어와의 충돌 감지
        {
            dialogueTextObject.SetActive(true); // 대사 오브젝트 활성화
            StopAllCoroutines(); // 진행 중인 모든 코루틴을 중지 (재입장 시 중복 방지)
            StartCoroutine(TypeSentence(dialogueText)); // 타이핑 효과 코루틴 시작
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 플레이어가 떠날 때
        {
            dialogueTextObject.SetActive(false); // 대사 오브젝트 비활성화
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueTextComponent.text = ""; // 텍스트 초기화
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueTextComponent.text += letter; // 한 글자씩 텍스트에 추가
            yield return new WaitForSeconds(0.05f); // 다음 글자 타이핑 딜레이
        }
    }

    // Update 메서드에서 UI Text의 위치를 NPC 위로 동적으로 업데이트
    private void Update()
    {
        if (dialogueTextObject.activeSelf)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2f, 0)); // NPC 위의 좌표
            dialogueTextObject.transform.position = screenPos;
        }
    }
}
