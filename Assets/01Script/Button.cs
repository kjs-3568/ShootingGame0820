using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public delegate void ButtonPush(string color);
    ButtonPush BP;

    public void OnClick(GameObject obj)
    {
        
    }
}
