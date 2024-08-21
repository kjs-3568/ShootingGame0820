using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 씬이 변경되더라도 인스턴스가 파괴되지않고, 유지되도록하는 싱글톤 
public class SingleTone<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Inst { get; private set; } // get은 누구든지, set은 프라이빗으로 접근하도록

    protected virtual void Awake() // 파생클래스에서 재정의 가능한 가상함수
    {
        if(Inst == null)
        {
            Inst = this as T; // T 타입의 자기가신 객체를 넣는다
            DontDestroyOnLoad(gameObject); // 자신의 객체를 파괴되지 않도록 설정.
        }
        else // 이미 객체가 존재하는데, 다시 만들려하는 경우
        {
            Destroy(gameObject);
        }
        DoAwake();
    }
    protected virtual void DoAwake() { } // 파생클래스에서 자신의 초기화에 필요한 로직.
}


// 2. 씬이 변경이 되면 인스턴스가 파괴되는 싱글톤
public class SingletoneDestroy<T> : MonoBehaviour where T : MonoBehaviour
{
    // 구조는 SingleTone과 동일
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
