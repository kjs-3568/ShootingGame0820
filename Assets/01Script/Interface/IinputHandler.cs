using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interface: 순수 가상함수로만 이뤄진 추상클래스. 다중상속이 가능
// 인테페이스 특징: 단독으로 인스턴스를 만들 수 없음.(상속으로서만 포함)
// 특징2 : 상속했을때 멤버의 메소드를 의무적으로 재정의 해야만 한다.

public interface IinputHandler
{
    // 입력을 받아오는 역할
    Vector2 GetInput();

}
