using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    public List<string> acceptedQuests = new List<string>(); // �÷��̾ ������ ����Ʈ ���

    // �÷��̾ ���ο� ����Ʈ�� �����ϴ� �޼���
    public void AcceptQuest(string questName)
    {
        acceptedQuests.Add(questName);
        Debug.Log("�÷��̾ ����Ʈ�� �����߽��ϴ�: " + questName);
        // ���⼭ �÷��̾��� ����Ʈ ��� UI�� ������Ʈ�� �� �ֽ��ϴ�.
    }
}
