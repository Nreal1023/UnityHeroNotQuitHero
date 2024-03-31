using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOn : MonoBehaviour
{
    public GameObject panel; 

    void Start()
    {
        Button startButton = GetComponent<Button>();
        startButton.onClick.AddListener(ActivatePanel);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivatePanel();
        }
    }

    public void ActivatePanel()
    {
        panel.SetActive(true);
    }

    void DeactivatePanel()
    {
        panel.SetActive(false);
    }
}
