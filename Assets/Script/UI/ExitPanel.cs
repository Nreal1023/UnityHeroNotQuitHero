using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPanel : MonoBehaviour
{
    public GameObject panel; // Ȱ��ȭ�� �г�
    public Button exitButton; // EXIT ��ư
    public Button cancelButton; // CANCEL ��ư

    void Start()
    {
        // EXIT ��ư�� �̺�Ʈ ������ �߰�
        exitButton.onClick.AddListener(ExitGame);
        // CANCEL ��ư�� �̺�Ʈ ������ �߰�
        cancelButton.onClick.AddListener(DeactivatePanel);
    }

    void Update()
    {
        // ESC Ű�� ������ �� �г� ��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivatePanel();
        }
    }

    // EXIT ��ư�� ������ �� ���� ����
    void ExitGame()
    {
        Application.Quit();
    }

    // �г��� Ȱ��ȭ�ϴ� �Լ�
    public void ActivatePanel()
    {
        panel.SetActive(true);
    }

    // �г��� ��Ȱ��ȭ�ϴ� �Լ�
    public void DeactivatePanel()
    {
        panel.SetActive(false);
    }
}
