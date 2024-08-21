using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. ���� ����Ǵ��� �ν��Ͻ��� �ı������ʰ�, �����ǵ����ϴ� �̱��� 
public class SingleTone<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Inst { get; private set; } // get�� ��������, set�� �����̺����� �����ϵ���

    protected virtual void Awake() // �Ļ�Ŭ�������� ������ ������ �����Լ�
    {
        if(Inst == null)
        {
            Inst = this as T; // T Ÿ���� �ڱⰡ�� ��ü�� �ִ´�
            DontDestroyOnLoad(gameObject); // �ڽ��� ��ü�� �ı����� �ʵ��� ����.
        }
        else // �̹� ��ü�� �����ϴµ�, �ٽ� ������ϴ� ���
        {
            Destroy(gameObject);
        }
        DoAwake();
    }
    protected virtual void DoAwake() { } // �Ļ�Ŭ�������� �ڽ��� �ʱ�ȭ�� �ʿ��� ����.
}


// 2. ���� ������ �Ǹ� �ν��Ͻ��� �ı��Ǵ� �̱���
public class SingletoneDestroy<T> : MonoBehaviour where T : MonoBehaviour
{
    // ������ SingleTone�� ����
    public static T Inst { get; private set; }

    protected virtual void Awake()
    {
        if(Inst == null)
        {
            Inst = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
        DoAwake();
    }
    protected virtual void DoAwake() { }
}
