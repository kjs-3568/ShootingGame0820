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

    private void OnEnable() // ���� ������Ʈ�� ���ʷ� Ȱ��ȭ �� ��
    {
        ButtonTest.OnColorSet += RandomColorSet;
    }

    private void OnDisable() // ������Ʈ�� ��Ȱ��ȭ �� ��
    {
        ButtonTest.OnColorSet -= RandomColorSet;
    }
}

