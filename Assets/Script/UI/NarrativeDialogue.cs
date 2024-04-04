using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeDialogue : MonoBehaviour
{
    public TMP_Text speakerNameText; // ��ȭ ����� �̸��� ǥ���� TMP_Text
    public TMP_Text dialogueText; // ��ȭ ������ ǥ���� TMP_Text
    public string[] speakerNames; // ��ȭ�� �����ϴ� ��ȭ ������ �̸� �迭
    public Color[] speakerNameColors; // ��ȭ�� �����ϴ� ��ȭ ������ �̸� ���� �迭
    public string[] narrativeSentences; // ��ȭ ������� ���� �迭
    public float letterDelay = 0.1f; // �� ���ھ� ��Ÿ���� ������
    public float startDelay = 2f; // ��ȭ ���۱��� ����� �ð�
    public float fadeDuration = 1f; // ��ȭ ���۰� �Բ� �̹����� �������� ������ �ð�
    public AudioClip typingSound; // Ÿ���� �Ҹ�

    private int currentSentenceIndex = 0; // ���� ��ȭ ���� �ε���
    private bool spacePressed = false; // �����̽� �ٰ� ���ȴ��� ����
    private Image dialogueImage; // �̹��� ������Ʈ
    private AudioSource audioSource; // ����� �ҽ� ������Ʈ

    [CanBeNull] public PlayerMovement playerMovementScript; // �÷��̾� �������� ����ϴ� ��ũ��Ʈ
    private bool dialogueInProgress = true; // ��ȭ ���� �� ����

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // ����� �ҽ� ������Ʈ ��������
        dialogueImage = GetComponent<Image>(); // �̹��� ������Ʈ ��������

        // �÷��̾� �������� ����ϴ� ��ũ��Ʈ�� ã��
        playerMovementScript = FindObjectOfType<PlayerMovement>();
        // ��ȭ ���۱��� ���
        StartCoroutine(StartDialogueWithDelay());
    }

    IEnumerator StartDialogueWithDelay()
    {
        dialogueImage.color = new Color(1f, 1f, 1f, 0f); // �ʱ⿡ �̹����� ������ �����ϰ� ����

        yield return new WaitForSeconds(startDelay); // ��ȭ ���۱��� ���
        StartNarrativeDialogue(); // ��ȭ ����
    }

    void StartNarrativeDialogue()
    {
        // �����̼� ��ȭ ����
        StartCoroutine(ShowNarrativeDialogue());
    }

    IEnumerator ShowNarrativeDialogue()
    {

        // ��ȭ ���� ������ ��Ÿ���� ���� ����
        dialogueInProgress = true;

        // ��ȭ ���� �ʱ�ȭ
        dialogueText.text = "";
        speakerNameText.text = "";

        // ������ �̹����� �������ϰ� ����
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            dialogueImage.color = new Color32(29, 29, 29, (byte)(alpha * 255));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ��ȭ ����� ������� ǥ��
        while (currentSentenceIndex < narrativeSentences.Length)
        {
            string sentence = narrativeSentences[currentSentenceIndex];
            string speakerName = speakerNames[currentSentenceIndex];
            Color speakerColor = speakerNameColors[currentSentenceIndex]; // �ش� ������ ��ȭ ����� �̸� ����

            // ��ȭ ����� �̸��� ���� ����
            speakerNameText.text = speakerName;
            speakerNameText.color = speakerColor;

            // ��ȭ ������ �� ���ھ� ǥ��
            for (int i = 0; i < sentence.Length; i++)
            {
                // �����̽� �ٰ� ������ �� ��� �ؽ�Ʈ�� �� ���� ǥ��
                if (spacePressed)
                {
                    dialogueText.text = sentence; // ��� �ؽ�Ʈ�� �� ���� ǥ��
                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); // �����̽� �� ���� ������ ���
                    break; // ��ȭ ���� ǥ�� ����
                }

                dialogueText.text += sentence[i];
                audioSource.PlayOneShot(typingSound); // Ÿ���� �Ҹ� ���
                yield return new WaitForSeconds(letterDelay);
            }

            // ��ȭ ������ ������ �� �����̽� �ٸ� �����⸦ ��ٸ�
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            spacePressed = false; // �����̽� �� ���� ���� �ʱ�ȭ

            // ���� ��ȭ �������� �̵�
            currentSentenceIndex++;

            // ��ȭ ���� �ʱ�ȭ
            dialogueText.text = "";

        }

        // ��ȭ ����
        Debug.Log("��ȭ ����");
        dialogueInProgress = false;
        // ��ȭ �г��� �������� 0���� ����
        dialogueImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        // ��� �ؽ�Ʈ ĭ �ʱ�ȭ
        speakerNameText.text = "";
        dialogueText.text = "";
    }

    void Update()
    {
        // ��ȭ ���� ���� �� �÷��̾� ������ ���� ��ũ��Ʈ�� ��Ȱ��ȭ
        if (dialogueInProgress)
        {
            playerMovementScript.enabled = false;
        }
        else
        {
            // ��ȭ�� ����Ǹ� �÷��̾� ������ ���� ��ũ��Ʈ�� �ٽ� Ȱ��ȭ
            playerMovementScript.enabled = true;
        }

        // �����̽� �ٸ� ������ �����̽� �ٰ� �������� ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
        }
    }
}
