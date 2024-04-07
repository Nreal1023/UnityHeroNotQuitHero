using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLogic : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject lamp;
    public PlayerMovement playerMovemScript;

    private bool isPlayerNearby = false;
    private bool lampBlinking = false;

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            ToggleInventoryPanel();
            if (!lampBlinking)
            {
                lampBlinking = true;
                StartCoroutine(BlinkLamp());

                // 플레이어 스크립트 비활성화
                if (playerMovemScript != null)
                {
                    playerMovemScript.enabled = false;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (lampBlinking)
            {
                StopCoroutine(BlinkLamp());
                lampBlinking = false;
                lamp.SetActive(false);

                // 플레이어 스크립트 다시 활성화
                if (playerMovemScript != null)
                {
                    playerMovemScript.enabled = true;
                }
            }
        }
    }

    private void ToggleInventoryPanel()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    private IEnumerator BlinkLamp()
    {
        while (lampBlinking)
        {
            lamp.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            lamp.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}