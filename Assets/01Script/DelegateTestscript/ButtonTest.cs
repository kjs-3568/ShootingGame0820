using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    public delegate void RandomColorSet(Color color);
    public static event RandomColorSet OnColorSet;

    Button button;


    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(buttonHandle);
    }

    public void buttonHandle()
    {
        OnColorSet?.Invoke(Random.ColorHSV());
    }
}
