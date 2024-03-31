using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeDialogue : MonoBehaviour
{
    public TMP_Text speakerNameText; // 대화 상대의 이름을 표시할 TMP_Text
    public TMP_Text dialogueText; // 대화 내용을 표시할 TMP_Text
    public string[] speakerNames; // 대화에 참여하는 대화 상대들의 이름 배열
    public Color[] speakerNameColors; // 대화에 참여하는 대화 상대들의 이름 색상 배열
    public string[] narrativeSentences; // 대화 문장들을 담을 배열
    public float letterDelay = 0.1f; // 한 글자씩 나타나는 딜레이
    public float startDelay = 2f; // 대화 시작까지 대기할 시간
    public float fadeDuration = 1f; // 대화 시작과 함께 이미지의 투명도를 변경할 시간
    public AudioClip typingSound; // 타이핑 소리

    private int currentSentenceIndex = 0; // 현재 대화 문장 인덱스
    private bool spacePressed = false; // 스페이스 바가 눌렸는지 여부
    private Image dialogueImage; // 이미지 컴포넌트
    private AudioSource audioSource; // 오디오 소스 컴포넌트

    [CanBeNull] public PlayerMovement playerMovementScript; // 플레이어 움직임을 담당하는 스크립트
    private bool dialogueInProgress = true; // 대화 진행 중 여부

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // 오디오 소스 컴포넌트 가져오기
        dialogueImage = GetComponent<Image>(); // 이미지 컴포넌트 가져오기

        // 플레이어 움직임을 담당하는 스크립트를 찾음
        playerMovementScript = FindObjectOfType<PlayerMovement>();
        // 대화 시작까지 대기
        StartCoroutine(StartDialogueWithDelay());
    }

    IEnumerator StartDialogueWithDelay()
    {
        dialogueImage.color = new Color(1f, 1f, 1f, 0f); // 초기에 이미지를 완전히 투명하게 설정

        yield return new WaitForSeconds(startDelay); // 대화 시작까지 대기
        StartNarrativeDialogue(); // 대화 시작
    }

    void StartNarrativeDialogue()
    {
        // 나레이션 대화 시작
        StartCoroutine(ShowNarrativeDialogue());
    }

    IEnumerator ShowNarrativeDialogue()
    {

        // 대화 진행 중임을 나타내는 변수 설정
        dialogueInProgress = true;

        // 대화 내용 초기화
        dialogueText.text = "";
        speakerNameText.text = "";

        // 서서히 이미지를 불투명하게 만듦
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            dialogueImage.color = new Color32(29, 29, 29, (byte)(alpha * 255));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 대화 문장들 순서대로 표시
        while (currentSentenceIndex < narrativeSentences.Length)
        {
            string sentence = narrativeSentences[currentSentenceIndex];
            string speakerName = speakerNames[currentSentenceIndex];
            Color speakerColor = speakerNameColors[currentSentenceIndex]; // 해당 문장의 대화 상대의 이름 색상

            // 대화 상대의 이름과 색상 설정
            speakerNameText.text = speakerName;
            speakerNameText.color = speakerColor;

            // 대화 문장을 한 글자씩 표시
            for (int i = 0; i < sentence.Length; i++)
            {
                // 스페이스 바가 눌렸을 때 모든 텍스트를 한 번에 표시
                if (spacePressed)
                {
                    dialogueText.text = sentence; // 모든 텍스트를 한 번에 표시
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); // 스페이스 바 누를 때까지 대기
                    break; // 대화 문장 표시 종료
                }

                dialogueText.text += sentence[i];
                audioSource.PlayOneShot(typingSound); // 타이핑 소리 재생
                yield return new WaitForSeconds(letterDelay);
            }

            // 대화 문장이 끝났을 때 스페이스 바를 누르기를 기다림
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            spacePressed = false; // 스페이스 바 누름 상태 초기화

            // 다음 대화 문장으로 이동
            currentSentenceIndex++;

            // 대화 문장 초기화
            dialogueText.text = "";

        }

        // 대화 종료
        Debug.Log("대화 종료");
        dialogueInProgress = false;
        // 대화 패널의 투명도를 0으로 설정
        dialogueImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        // 모든 텍스트 칸 초기화
        speakerNameText.text = "";
        dialogueText.text = "";
    }

    void Update()
    {
        // 대화 진행 중일 때 플레이어 움직임 제어 스크립트를 비활성화
        if (dialogueInProgress)
        {
            playerMovementScript.enabled = false;
        }
        else
        {
            // 대화가 종료되면 플레이어 움직임 제어 스크립트를 다시 활성화
            playerMovementScript.enabled = true;
        }

        // 스페이스 바를 누르면 스페이스 바가 눌렸음을 저장
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
        }
    }
}
