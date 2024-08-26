using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, Imovement
{
    private bool isMoving = false; // ������������ �̵�

    [SerializeField]
    private float moveSpeed = 5f; // �÷��̾��� �̵� �ӵ�

    private Vector2 minArea = new Vector2(-2f, -4.5f);
    private Vector2 maxArea = new Vector2(2f, 0f);

    private Vector3 moveDelta; // �̵��� ����� ���� ��

    public void Move(Vector2 direction) // �̵�����
    {
        if(isMoving)
        {
            moveDelta = new Vector3(direction.x, direction.y, 0f) * (moveSpeed * Time.deltaTime); // ()�� �ϸ� �ɿ��� �� ȿ����.

            Vector3 newPosition = transform.position + moveDelta; // ����ġ + �̵���

            // �̵����� ����
            newPosition.x = Mathf.Clamp(newPosition.x, minArea.x, maxArea.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minArea.y, maxArea.y);

            transform.position = newPosition;
        }
    }

    public void SetEnabled(bool newEnable)
    {
        isMoving = newEnable;
    }
}
