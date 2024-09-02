using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorchange : MonoBehaviour
{
    SpriteRenderer sr;

    public void RandomColorSet(Color color)
    {
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        if (sr != null)
            sr.color = color;
    }

    private void OnEnable() // 게임 오브젝트가 최초로 활성화 될 때
    {
        ButtonTest.OnColorSet += RandomColorSet;
    }

    private void OnDisable() // 오브젝트가 비활성화 될 때
    {
        ButtonTest.OnColorSet -= RandomColorSet;
    }
}

