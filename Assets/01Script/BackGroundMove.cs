using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBackGroundScroller
{
    void Scroll(float deltaTime); // ��ŸŸ���� �Է¹޾Ƽ� ������Ʈ�� �̵���Ű�� ���
    void ResetPosition(); // Ư�� ��ġ���� ����������. �ʱ���ġ�� �ڸ��� �����ϴ� ���
    void SetScrollSpeed(float newSpeed); // ��ũ�� �Ǵ� �ӵ��� ��ȭ�ϴ� ���
}

public class BackGroundMove : MonoBehaviour, IBackGroundScroller // ���߻�ӹޱ� ����
{
    [SerializeField]
    float scrollSpeed = 0f;
    private Vector3 startPos = new Vector3(0f, 12.5f, 0f); // ���� �Ǿ�� �ϴ� ��ġ
    private float resetPositionY = -12.5f; // ȭ���� ����� ������ �ؾ�����, ���� �Ǵ��ϴ� ����
    public void ResetPosition()
    {
        transform.position = startPos;
    }
    public void Scroll(float deltaTime)
    {
        transform.position += Vector3.down * scrollSpeed * Time.deltaTime;
        if (transform.position.y < resetPositionY)
            ResetPosition();
    }
    public void SetScrollSpeed(float newSpeed)
    {
        scrollSpeed = newSpeed;
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
