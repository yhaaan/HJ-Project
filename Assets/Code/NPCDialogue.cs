using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialogueTextObject;
    public List<string> dialogueTexts = new List<string> { "안녕하세요! NPC입니다.", "오늘 날씨가 참 좋네요.", "다음에 또 만나요!" }; 
    public Text dialogueTextComponent;
    public float sentenceDelay = 2.0f; // 대사 사이의 지연 시간

    private int currentSentenceIndex = 0; 
    private bool isPlayerNear = false;

    private void Start()
    {
        dialogueTextComponent = dialogueTextObject.GetComponent<Text>();
        dialogueTextObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
            dialogueTextObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(DisplaySentences());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = false;
            dialogueTextObject.SetActive(false);
            StopAllCoroutines(); 
        }
    }

    IEnumerator DisplaySentences()
    {
        while (isPlayerNear)
        {
            yield return StartCoroutine(TypeSentence(dialogueTexts[currentSentenceIndex]));

            currentSentenceIndex = (currentSentenceIndex + 1) % dialogueTexts.Count;
            yield return new WaitForSeconds(sentenceDelay); // 다음 대사로 넘어가기 전에 대기
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueTextComponent.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueTextComponent.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void Update()
    {
        if (isPlayerNear)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            dialogueTextObject.transform.position = screenPos;
        }
    }
}