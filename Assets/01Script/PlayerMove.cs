using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, Imovement
{
    private bool isMoving = false; // 켜져있을때만 이동

    [SerializeField]
    private float moveSpeed = 5f; // 플레이어의 이동 속도

    private Vector2 minArea = new Vector2(-2f, -4.5f);
    private Vector2 maxArea = new Vector2(2f, 0f);

    private Vector3 moveDelta; // 이동량 계산을 위한 것

    public void Move(Vector2 direction) // 이동구현
    {
        if(isMoving)
        {
            moveDelta = new Vector3(direction.x, direction.y, 0f) * (moveSpeed * Time.deltaTime); // ()를 하면 쪼오금 더 효율적.

            Vector3 newPosition = transform.position + moveDelta; // 현위치 + 이동량

            // 이동범위 제한
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
