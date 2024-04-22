// BoxLogic.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLogic : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject lamp;
    public PlayerMovement playerMovementScript;

    private bool isPlayerNearby = false;
    private bool lampBlinking = false;
    private bool inventoryOpen = false;

    private void Start()
    {
        playerMovementScript = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            ToggleInventoryPanel();
            inventoryOpen = !inventoryOpen;

            if (!lampBlinking)
            {
                StartBlinking();
            }

            // 인벤토리 창이 열릴 때 플레이어 움직임 제한
            if (inventoryOpen)
            {
                playerMovementScript.RestrictMovement();
            }
            else
            {
                playerMovementScript.ReleaseMovement();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = true;
            StartBlinking();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = false;
            StopBlinking();
            playerMovementScript.ReleaseMovement();
        }
    }

    private void ToggleInventoryPanel()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    private void StartBlinking()
    {
        lampBlinking = true;
        StartCoroutine(BlinkLamp());
    }

    private void StopBlinking()
    {
        lampBlinking = false;
        StopCoroutine(BlinkLamp());
        lamp.SetActive(false);
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
