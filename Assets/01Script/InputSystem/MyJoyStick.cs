using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyJoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Transform handle; // 유저의 조작방향을 시각정으로 체크해보기 위해서.
    private Transform pivot; // 범위를 벗어나는지 확인하기 위한 기준점
    private float distance; // handle이 조이스틱의 중심부에서 얼만큼 멀어졌는지 거리를 관리.
    private Vector2 direction;
    public Vector2 Direction => direction; // => : 애로우 연산자. 포인터 개념
                                           // direction의 값을 외부에서 읽기만 가능하도록 Direction 생성.(캡슐화)

    private void Awake()
    {
        InitJoystick(); // 임의로 호출. 추후에는 게임매니저에서 관리하도록 할 것임
    }

    public void InitJoystick()
    {
        handle = transform.Find("Handle");
        pivot = transform.Find("Pivot");
    }

    public void OnDrag(PointerEventData eventData) // 마우스의 위치로 handle의 위치를 갱신하고,
                                                   // 갱신하면서 pivot보다 더 멀어지지않게 관리를 하고,
                                                   // direction의 값을 생성.
    {
        if(handle != null && pivot != null)
        {
            // 조이스틱의 중심과 pivot 사이의 거리 계산
            distance = Vector2.Distance(transform.position, pivot.position);
            // Distance 메소드가 성능이 좀 구림. (transform.position - pivot.position).sqrMagnitude : 단순거리비교 할 땐 이게 좀 더 나은 메소드

            handle.position = eventData.position; // 현 입력값으로 핸들을 움직임

            float currentDist = Vector2.Distance(transform.position, handle.position);

            direction = (handle.position - transform.position).normalized; // 방향벡터를 만들고, 그 방향벡터의 길이를 1로 만드는 노멀라이즈.

            if (currentDist > distance) // 범위 밖으로 핸들이 이동했을때
            {
                handle.localPosition = direction * distance; // pivot까지만 핸들위치를 고정.
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    
    public void OnPointerUp(PointerEventData eventData) // handle 초기 위치인 로컬 기준에 0, 0, 0으로 변경
    {
        direction = Vector2.zero;
        handle.localPosition = Vector3.zero;
    }
}
