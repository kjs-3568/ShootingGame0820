using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyJoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Transform handle; // ������ ���۹����� �ð������� üũ�غ��� ���ؼ�.
    private Transform pivot; // ������ ������� Ȯ���ϱ� ���� ������
    private float distance; // handle�� ���̽�ƽ�� �߽ɺο��� ��ŭ �־������� �Ÿ��� ����.
    private Vector2 direction;
    public Vector2 Direction => direction; // => : �ַο� ������. ������ ����
                                           // direction�� ���� �ܺο��� �б⸸ �����ϵ��� Direction ����.(ĸ��ȭ)

    private void Awake()
    {
        InitJoystick(); // ���Ƿ� ȣ��. ���Ŀ��� ���ӸŴ������� �����ϵ��� �� ����
    }

    public void InitJoystick()
    {
        handle = transform.Find("Handle");
        pivot = transform.Find("Pivot");
    }

    public void OnDrag(PointerEventData eventData) // ���콺�� ��ġ�� handle�� ��ġ�� �����ϰ�,
                                                   // �����ϸ鼭 pivot���� �� �־������ʰ� ������ �ϰ�,
                                                   // direction�� ���� ����.
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    
    public void OnPointerUp(PointerEventData eventData) // handle �ʱ� ��ġ�� ���� ���ؿ� 0, 0, 0���� ����
    {
    }
}
