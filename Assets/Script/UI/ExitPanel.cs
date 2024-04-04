using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPanel : MonoBehaviour
{
    public GameObject panel; // 활성화할 패널
    public Button exitButton; // EXIT 버튼
    public Button cancelButton; // CANCEL 버튼

    void Start()
    {
        // EXIT 버튼에 이벤트 리스너 추가
        exitButton.onClick.AddListener(ExitGame);
        // CANCEL 버튼에 이벤트 리스너 추가
        cancelButton.onClick.AddListener(DeactivatePanel);
    }

    void Update()
    {
        // ESC 키를 눌렀을 때 패널 비활성화
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivatePanel();
        }
    }

    // EXIT 버튼을 눌렀을 때 게임 종료
    void ExitGame()
    {
        Application.Quit();
    }

    // 패널을 활성화하는 함수
    public void ActivatePanel()
    {
        panel.SetActive(true);
    }

    // 패널을 비활성화하는 함수
    public void DeactivatePanel()
    {
        panel.SetActive(false);
    }
}
