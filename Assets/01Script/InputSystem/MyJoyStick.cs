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
        if(handle != null && pivot != null)
        {
            // ���̽�ƽ�� �߽ɰ� pivot ������ �Ÿ� ���
            distance = Vector2.Distance(transform.position, pivot.position);
            // Distance �޼ҵ尡 ������ �� ����. (transform.position - pivot.position).sqrMagnitude : �ܼ��Ÿ��� �� �� �̰� �� �� ���� �޼ҵ�

            handle.position = eventData.position; // �� �Է°����� �ڵ��� ������

            float currentDist = Vector2.Distance(transform.position, handle.position);

            direction = (handle.position - transform.position).normalized; // ���⺤�͸� �����, �� ���⺤���� ���̸� 1�� ����� ��ֶ�����.

            if (currentDist > distance) // ���� ������ �ڵ��� �̵�������
            {
                handle.localPosition = direction * distance; // pivot������ �ڵ���ġ�� ����.
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    
    public void OnPointerUp(PointerEventData eventData) // handle �ʱ� ��ġ�� ���� ���ؿ� 0, 0, 0���� ����
    {
        direction = Vector2.zero;
        handle.localPosition = Vector3.zero;
    }
}
