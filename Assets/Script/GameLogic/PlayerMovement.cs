using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �÷��̾� �̵� �ӵ�

    private Rigidbody2D rb;
    private float tileSize = 16f; // Ÿ���� ũ��

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // ������ �Է��� �޽��ϴ�.
        float moveInputX = Input.GetAxisRaw("Horizontal");
        float moveInputY = Input.GetAxisRaw("Vertical");

        // �Է��� �߻��ϸ� Ÿ�� ũ�⸸ŭ �̵��մϴ�.
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
        // �����̴� ���⿡ ���� ������ ���� �����մϴ�.
        Vector2 movement = new Vector2(tileSize * direction, 0f) * moveSpeed * Time.deltaTime;

        // Rigidbody�� �̿��Ͽ� �÷��̾ �����Դϴ�.
        rb.MovePosition(rb.position + movement);
    }

    private void MoveVertical(float direction)
    {
        // �����̴� ���⿡ ���� ������ ���� �����մϴ�.
        Vector2 movement = new Vector2(0f, tileSize * direction) * moveSpeed * Time.deltaTime;

        // Rigidbody�� �̿��Ͽ� �÷��̾ �����Դϴ�.
        rb.MovePosition(rb.position + movement);
    }
}
