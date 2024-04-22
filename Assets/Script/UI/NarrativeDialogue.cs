using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NarrativeDialogue : MonoBehaviour
{
    public TMP_Text speakerNameText;
    public TMP_Text questTMP;
    public String questDescription;
    public TMP_Text dialogueText;
    public string[] speakerNames; 
    public Color[] speakerNameColors; 
    public string[] narrativeSentences; 
    public float letterDelay = 0.1f; 
    public float startDelay = 2f; 
    public float fadeDuration = 1f; 
    public AudioClip typingSound;

    private String _questDescription;
    private int currentSentenceIndex = 0; 
    private bool spacePressed = false; 
    private Image dialogueImage;
    private AudioSource audioSource; 

    [CanBeNull] public PlayerMovement playerMovementScript;
    private bool dialogueInProgress = true;

    public GameObject questPanel;

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

        if (questPanel != null)
        {
            questPanel.SetActive(false);
        }

        yield return new WaitForSeconds(startDelay);
        StartNarrativeDialogue();
    }

    void StartNarrativeDialogue()
    {
        StartCoroutine(ShowNarrativeDialogue());
    }


    IEnumerator ShowNarrativeDialogue()
{
    dialogueInProgress = true;

    dialogueText.text = "";
    speakerNameText.text = "";

    float elapsedTime = 0f;
    while (elapsedTime < fadeDuration)
    {
        float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
        dialogueImage.color = new Color32(29, 29, 29, (byte)(alpha * 255));
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    while (currentSentenceIndex < narrativeSentences.Length)
    {
        string sentence = narrativeSentences[currentSentenceIndex];
        string speakerName = speakerNames[currentSentenceIndex];
        Color speakerColor = speakerNameColors[currentSentenceIndex];

        speakerNameText.text = speakerName;
        speakerNameText.color = speakerColor;

        for (int i = 0; i < sentence.Length; i++)
        {
            if (spacePressed)
            {
                dialogueText.text = sentence;
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                break;
            }

            dialogueText.text += sentence[i];
            audioSource.PlayOneShot(typingSound);
            yield return new WaitForSeconds(letterDelay);
        }

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        spacePressed = false;

        currentSentenceIndex++;
        dialogueText.text = "";
    }
    
    Debug.Log("대화 종료");
    dialogueInProgress = false;

    if (questPanel != null)
    {
        questPanel.SetActive(true);
        questTMP.text = questDescription;

        questPanel.transform.DOMoveY(0f, 1f).SetEase(Ease.OutBounce);
    }

    dialogueImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
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