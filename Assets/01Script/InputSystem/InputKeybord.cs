using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeybord : MonoBehaviour, IinputHandler
{
    public Vector2 GetInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
