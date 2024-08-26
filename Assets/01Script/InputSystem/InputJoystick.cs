using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputJoystick : MonoBehaviour, IinputHandler
{
    private MyJoyStick joystick;
    private void Awake()
    {
        joystick = FindAnyObjectByType<MyJoyStick>(); // FindAnyObjectByType: �� ���� ������ Ÿ���� ������Ʈ�� �Ѱ� ã�ƿ�.
    }
    public void InitJoystick()
    {
        joystick = FindAnyObjectByType<MyJoyStick>();
        if (joystick == null)
            Debug.Log("InputJoystick.sx - InitJoystick() - ���̽�ƽ ���� ����!");
    }
    public Vector2 GetInput()
    {
        return joystick.Direction; // new Vector2(joystick.Direction.x, joystick.Direction.y);
    }
}
