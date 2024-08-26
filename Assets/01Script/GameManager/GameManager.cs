using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임의 전반적인 흐름을 관리.
// 1. 게임의 시작, 게임의 중지, 게임을 종료.
// 2. 사운드 관리자
// 3. 동적로딩 : 에셋 로딩 즉,rom데이터를 Ram공간으로 불러오는 작업
// 4. 씬 관리 : 씬 변경시에 데이터를 주고받고, 게임종료되면 씬을 변경시키는 등.
// 5. (임시) 입력을 받아서 캐릭터에 전달.(IinputHandler로부터 방향정보를 전달받아서, 캐릭터 movement에 전달)

public class GameManager : SingleTone<GameManager> // 싱글톤을 상속받음
{
    ScrollManager scrollManager;

    private IinputHandler inputHandler;
    private Imovement movementController;

    GameObject obj; // 메소드 전반에서 오브젝 찾을때.

    private void Start()
    {
        scrollManager = GameObject.FindAnyObjectByType<ScrollManager>();

        StartCoroutine(GameStart());

        LoadSceneInit(); // (임시) 나중에 씬 변경이 될 때 호출되도록변경할 것입
    }

    private void LoadSceneInit() // 씬이 변경이 될 때, 변경된 UI에서 joystick 찾아오고
                                 // 씬에 따라서 변경되는 Player객체도 찾아서 참조.
    {
        inputHandler = GetComponent<InputKeybord>(); // 개발과정용 임시코드

        obj = FindObjectsByType<PlayerMove>(FindObjectsSortMode.None)[0].gameObject; // 임시코드
        if (obj != null)
        {
            if (!obj.TryGetComponent<Imovement>(out movementController))
                Debug.Log("GameManager.cs - LoadSceneInit() - movementController 참조실패!");
        }

//#if UNITY_EDITOR
//        inputHandler = GetComponent<InputKeybord>();
//#elif UNITY_ANDROID
//        inputHandler = GetComponent<InputKeybord>();
//#endif

    }

    private void Update()
    {
        if (inputHandler != null)
            movementController.Move(inputHandler.GetInput());
    }


    IEnumerator GameStart() // 게임시작 시 연출을 위해 딜레이를 줌.
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("게임 준비");
        yield return new WaitForSeconds(2f);
        scrollManager?.SetScrollSpeed(4f); // ?. : null 체크 연산자. if(n != null)과 같은 역할.
    }
}
