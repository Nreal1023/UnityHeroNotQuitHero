using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialog : MonoBehaviour
{
    // NarrativeDialogue 스크립트의 인스턴스
    public NarrativeDialogue narrativeDialogue;

    // 대화 정보
    public string[] customSpeakerNames;
    public Color[] customSpeakerNameColors;
    public string[] customNarrativeSentences;
    public AudioClip customTypingSound;

    void Start()
    {
        // NarrativeDialogue 스크립트의 인스턴스 가져오기
        narrativeDialogue = FindObjectOfType<NarrativeDialogue>();

        // speakerNames, speakerNameColors, narrativeSentences, typingSound 할당
        narrativeDialogue.speakerNames = customSpeakerNames;
        narrativeDialogue.speakerNameColors = customSpeakerNameColors;
        narrativeDialogue.narrativeSentences = customNarrativeSentences;
        narrativeDialogue.typingSound = customTypingSound;
    }
}
