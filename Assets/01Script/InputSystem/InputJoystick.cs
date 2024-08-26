using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputJoystick : MonoBehaviour, IinputHandler
{
    private MyJoyStick joystick;
    private void Awake()
    {
        joystick = FindAnyObjectByType<MyJoyStick>(); // FindAnyObjectByType: 씬 내의 동일한 타입의 오브젝트를 한개 찾아옴.
    }
    public void InitJoystick()
    {
        joystick = FindAnyObjectByType<MyJoyStick>();
        if (joystick == null)
            Debug.Log("InputJoystick.sx - InitJoystick() - 조이스틱 참조 실패!");
    }
    public Vector2 GetInput()
    {
        return joystick.Direction; // new Vector2(joystick.Direction.x, joystick.Direction.y);
    }
}
