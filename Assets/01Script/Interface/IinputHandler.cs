using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interface: ���� �����Լ��θ� �̷��� �߻�Ŭ����. ���߻���� ����
// �������̽� Ư¡: �ܵ����� �ν��Ͻ��� ���� �� ����.(������μ��� ����)
// Ư¡2 : ��������� ����� �޼ҵ带 �ǹ������� ������ �ؾ߸� �Ѵ�.

public interface IinputHandler
{
    // �Է��� �޾ƿ��� ����
    Vector2 GetInput();

}
