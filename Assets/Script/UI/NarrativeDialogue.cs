using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeDialogue : MonoBehaviour
{
    public TMP_Text speakerNameText;
    public TMP_Text dialogueText;
    public string[] speakerNames; 
    public Color[] speakerNameColors; 
    public string[] narrativeSentences; 
    public float letterDelay = 0.1f; 
    public float startDelay = 2f; 
    public float fadeDuration = 1f; 
    public AudioClip typingSound;

    private int currentSentenceIndex = 0; 
    private bool spacePressed = false; 
    private Image dialogueImage;
    private AudioSource audioSource; 

    [CanBeNull] public PlayerMovement playerMovementScript;
    private bool dialogueInProgress = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dialogueImage = GetComponent<Image>();


        playerMovementScript = FindObjectOfType<PlayerMovement>();

        StartCoroutine(StartDialogueWithDelay());
    }

    IEnumerator StartDialogueWithDelay()
    {
        dialogueImage.color = new Color(1f, 1f, 1f, 0f); 

        yield return new WaitForSeconds(startDelay);
        StartNarrativeDialogue();
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
        if (dialogueInProgress)
        {
            playerMovementScript.enabled = false;
        }
        else
        {
            playerMovementScript.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
        }
    }
}
