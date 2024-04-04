using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 플레이어 이동 속도

    private Rigidbody2D rb;
    private float tileSize = 16f; // 타일의 크기

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 움직임 입력을 받습니다.
        float moveInputX = Input.GetAxisRaw("Horizontal");
        float moveInputY = Input.GetAxisRaw("Vertical");

        // 입력이 발생하면 타일 크기만큼 이동합니다.
        if (moveInputX != 0)
        {
            MoveHorizontal(moveInputX);
        }
        else if (moveInputY != 0)
        {
            MoveVertical(moveInputY);
        }
    }

    private void MoveHorizontal(float direction)
    {
        // 움직이는 방향에 따라서 움직일 양을 설정합니다.
        Vector2 movement = new Vector2(tileSize * direction, 0f) * moveSpeed * Time.deltaTime;

        // Rigidbody를 이용하여 플레이어를 움직입니다.
        rb.MovePosition(rb.position + movement);
    }

    private void MoveVertical(float direction)
    {
        // 움직이는 방향에 따라서 움직일 양을 설정합니다.
        Vector2 movement = new Vector2(0f, tileSize * direction) * moveSpeed * Time.deltaTime;

        // Rigidbody를 이용하여 플레이어를 움직입니다.
        rb.MovePosition(rb.position + movement);
    }
}
