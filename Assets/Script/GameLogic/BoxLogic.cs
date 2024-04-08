using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLogic : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject lamp;

    private bool isPlayerNearby = false;
    private bool lampBlinking = false;

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            ToggleInventoryPanel();
            if (!lampBlinking)
            {
                StartBlinking();
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