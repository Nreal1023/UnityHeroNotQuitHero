using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class NPC_Dialog : MonoBehaviour
{
    // NarrativeDialogue 스크립트에 대한 참조
    public NarrativeDialogue narrativeDialogue;

    // 사용자 정의 변수
    public string[] customSpeakerNames;
    public Color[] customSpeakerNameColors;
    public string[] customNarrativeSentences;
    public AudioClip customTypingSound;
    public TMP_Text questTMPText;
    public String customQuestDescription;
    public GameObject customQuestPanel; // 새로운 questPanel 변수

    void Start()
    {
        // NarrativeDialogue 스크립트의 인스턴스를 찾음
        narrativeDialogue = FindObjectOfType<NarrativeDialogue>();

        // speakerNames, speakerNameColors, narrativeSentences, typingSound 변수에 사용자 정의 값을 할당
        narrativeDialogue.speakerNames = customSpeakerNames;
        narrativeDialogue.speakerNameColors = customSpeakerNameColors;
        narrativeDialogue.narrativeSentences = customNarrativeSentences;
        narrativeDialogue.typingSound = customTypingSound;
        narrativeDialogue.questTMP = questTMPText;
        narrativeDialogue.questDescription = customQuestDescription;

        // questPanel 변수에 사용자 정의 값 할당
        narrativeDialogue.questPanel = customQuestPanel;
    }
}