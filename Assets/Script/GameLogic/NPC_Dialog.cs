using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialog : MonoBehaviour
{
    // NarrativeDialogue ��ũ��Ʈ�� �ν��Ͻ�
    public NarrativeDialogue narrativeDialogue;

    // ��ȭ ����
    public string[] customSpeakerNames;
    public Color[] customSpeakerNameColors;
    public string[] customNarrativeSentences;
    public AudioClip customTypingSound;

    void Start()
    {
        // NarrativeDialogue ��ũ��Ʈ�� �ν��Ͻ� ��������
        narrativeDialogue = FindObjectOfType<NarrativeDialogue>();

        // speakerNames, speakerNameColors, narrativeSentences, typingSound �Ҵ�
        narrativeDialogue.speakerNames = customSpeakerNames;
        narrativeDialogue.speakerNameColors = customSpeakerNameColors;
        narrativeDialogue.narrativeSentences = customNarrativeSentences;
        narrativeDialogue.typingSound = customTypingSound;
    }
}
