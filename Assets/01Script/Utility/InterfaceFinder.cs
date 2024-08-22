using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���忡 �ִ� Ư�� �������̽��� ��� Ž���ؿ���.
// Interface: ���� �����Լ��θ� �̷��� �߻�Ŭ����. ���߻���� ����
public class InterfaceFinder : MonoBehaviour
{
    //FindObjectsOfType<> : FindObjectsByType<> ���� ������. ����� �� �� ���ĵ�. �׷��� ����Ƽ ������ ���� ���� �� ����
    //FindObjectsByType<> : ���� �� ���� ����. ������ ���ı���� ����. �ڿ� (FindObjectsSortMode.Ÿ��)�� ���� ��
    public static List<T> FindObjectsOfInterface<T>() where T : class // public static�̱� ������ ��𼭵� �� �� ����
    {
        MonoBehaviour[] allObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        List<T> interfaceObjects = new List<T>();

        foreach (var obj in allObjects) // var : Ȯ���� Ÿ���� �ƴ�, Ÿ���� �θ������ϰ� ������ �� ����
        {
            // if(obj is T) : obj�� T Ÿ���� ��ü���� Ȯ����. is�� ĳ����(����ȯ) ��ɾ�
            if (obj is T interfaceObj) // obj�� T Ÿ������ ĳ�����ؼ� �����ϸ�,interfaceObj�� �����Ѵ�.
            {
                interfaceObjects.Add(interfaceObj);
            }
        }
        return interfaceObjects;
    }
}
