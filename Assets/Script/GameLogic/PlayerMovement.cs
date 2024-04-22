// PlayerMovement.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private float tileSize = 16f;
    private bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (canMove) // 플레이어가 움직일 수 있는 상태인 경우에만 움직임 처리
        {
            float moveInputX = Input.GetAxisRaw("Horizontal");
            float moveInputY = Input.GetAxisRaw("Vertical");

            if (moveInputX != 0)
            {
                MoveHorizontal(moveInputX);
            }
            else if (moveInputY != 0)
            {
                MoveVertical(moveInputY);
            }
        }
    }

    private void MoveHorizontal(float direction)
    {
        Vector2 movement = new Vector2(tileSize * direction, 0f) * moveSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + movement);
    }

    private void MoveVertical(float direction)
    {
        Vector2 movement = new Vector2(0f, tileSize * direction) * moveSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + movement);
    }
    
    public void RestrictMovement()
    {
        canMove = false;
        Debug.Log("움직임 제한");
    }
    
    public void ReleaseMovement()
    {
        canMove = true;
        Debug.Log("움직임 제한 해제");
    }
}