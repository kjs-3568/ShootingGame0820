using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임의 전반적인 흐름을 관리.
// 1. 게임의 시작, 게임의 중지, 게임을 종료.
// 2. 사운드 관리자
// 3. 동적로딩 : 에셋 로딩 즉,rom데이터를 Ram공간으로 불러오는 작업
// 4. 씬 관리 : 씬 변경시에 데이터를 주고받고, 게임종료되면 씬을 변경시키는 등.

public class GameManager : SingleTone<GameManager> // 싱글톤을 상속받음
{
    ScrollManager scrollManager;

    private void Start()
    {
        scrollManager = GameObject.FindAnyObjectByType<ScrollManager>();

        StartCoroutine(GameStart());
    }

    IEnumerator GameStart() // 게임시작 시 연출을 위해 딜레이를 줌.
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("게임 준비");
        yield return new WaitForSeconds(2f);
        scrollManager?.SetScrollSpeed(4f); // ?. : null 체크 연산자. if(n != null)과 같은 역할.
    }
}
