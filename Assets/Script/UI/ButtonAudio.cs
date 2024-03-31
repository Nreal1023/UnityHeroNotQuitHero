using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour
{
    public AudioClip hoverSound;
    public AudioClip clickSound;
    public AudioClip escSound;

    public Button[] buttons;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        foreach (Button button in buttons)
        {
            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entryHover = new EventTrigger.Entry();
            entryHover.eventID = EventTriggerType.PointerEnter;
            entryHover.callback.AddListener((eventData) => { PlaySound(hoverSound); });
            trigger.triggers.Add(entryHover);

            button.onClick.AddListener(() => { PlaySound(clickSound); });
        }
    }

    void Update()
    {
        // ESC 키를 눌렀을 때 소리 재생
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlaySound(escSound);
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
}
