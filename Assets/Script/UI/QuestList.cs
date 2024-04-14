using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    public List<string> acceptedQuests = new List<string>(); // 플레이어가 수락한 퀘스트 목록

    // 플레이어가 새로운 퀘스트를 수락하는 메서드
    public void AcceptQuest(string questName)
    {
        acceptedQuests.Add(questName);
        Debug.Log("플레이어가 퀘스트를 수락했습니다: " + questName);
        // 여기서 플레이어의 퀘스트 목록 UI를 업데이트할 수 있습니다.
    }
}
