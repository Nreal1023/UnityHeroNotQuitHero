using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    public List<string> acceptedQuests = new List<string>();
    public List<string> laterQuests = new List<string>();

    public void AcceptQuest(string questName)
    {
        acceptedQuests.Add(questName);
        Debug.Log("퀘스트가 수락됨: " + questName);
        gameObject.SetActive(false);
    }

    public void AddToLaterQuests(string questName)
    {
        laterQuests.Add(questName);
        Debug.Log("퀘스트가 나중에 하기 리스트에 추가됨: " + questName);
        gameObject.SetActive(false);
    }
}