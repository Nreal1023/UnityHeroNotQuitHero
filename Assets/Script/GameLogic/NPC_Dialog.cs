using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialog : MonoBehaviour
{
    // NarrativeDialogue ???????? ??????
    public NarrativeDialogue narrativeDialogue;

    // ??? ????
    public string[] customSpeakerNames;
    public Color[] customSpeakerNameColors;
    public string[] customNarrativeSentences;
    public AudioClip customTypingSound;

    void Start()
    {
        // NarrativeDialogue ???????? ?????? ????????
        narrativeDialogue = FindObjectOfType<NarrativeDialogue>();

        // speakerNames, speakerNameColors, narrativeSentences, typingSound ???
        narrativeDialogue.speakerNames = customSpeakerNames;
        narrativeDialogue.speakerNameColors = customSpeakerNameColors;
        narrativeDialogue.narrativeSentences = customNarrativeSentences;
        narrativeDialogue.typingSound = customTypingSound;
    }
}
