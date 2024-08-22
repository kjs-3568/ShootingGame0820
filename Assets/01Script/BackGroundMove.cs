using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBackGroundScroller
{
    void Scroll(float deltaTime); // 델타타임을 입력받아서 오브젝트를 이동시키는 기능
    void ResetPosition(); // 특정 위치까지 내려갔을때. 초기위치로 자리를 리셋하는 기능
    void SetScrollSpeed(float newSpeed); // 스크롤 되는 속도를 변화하는 기능
}

public class BackGroundMove : MonoBehaviour, IBackGroundScroller // 다중상속받기 가능
{
    [SerializeField]
    float scrollSpeed = 0f;
    private Vector3 startPos = new Vector3(0f, 12.5f, 0f); // 리셋 되어야 하는 위치
    private float resetPositionY = -12.5f; // 화면을 벗어나서 리셋을 해야할지, 말지 판단하는 높이
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
