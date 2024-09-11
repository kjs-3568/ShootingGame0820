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
    private PlayerController pc;
    private MeteoManager meteoManager;
    private SpawnManager spawnManager;
    private ScoreManager scoreManager;

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

        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null)
            Debug.Log("GameManager.cs - LoadSceneInit() - pc 참조실패!");

        scrollManager = FindAnyObjectByType<ScrollManager>();
        spawnManager = FindAnyObjectByType<SpawnManager>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
        meteoManager = FindAnyObjectByType<MeteoManager>();


        //#if UNITY_EDITOR
        //        inputHandler = GetComponent<InputKeybord>();
        //#elif UNITY_ANDROID
        //        inputHandler = GetComponent<InputKeybord>();
        //#endif

    }

    private void Update()
    {
        if (inputHandler != null)
        {
            pc?.CustomUpdate(inputHandler.GetInput());
        }
    }


    IEnumerator GameStart() // 게임시작 시 연출을 위해 딜레이를 줌.
    {
        yield return null;
        Debug.Log("게임 데이터 초기화");
        scoreManager?.InitScoreSet();
        scoreManager.OnDiedPlayer += PlayerDiedProcess;
        yield return new WaitForSeconds(2f);
        pc?.StartGame(); // 게임시작하면 공격을 시작하고, 유저의 입력 받아옴
        Debug.Log("플레이어 컨트롤 On");
        yield return new WaitForSeconds(1f);
        scrollManager?.SetScrollSpeed(4f);
        Debug.Log("배경 스크롤 시작");
        yield return new WaitForSeconds(2f);
        spawnManager?.InitSpawnManager();
        Debug.Log("몬스터 스폰 시작");
        yield return new WaitForSeconds(5f);
        meteoManager.StartSpawnMeteo();
        Debug.Log("메테오 스폰 시작");
    }

    private void PlayerDiedProcess()
    {
        StopAllCoroutines();
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        yield return null;
        scoreManager.OnDiedPlayer -= PlayerDiedProcess;
        pc?.OverGame();
        scrollManager.SetScrollSpeed(0f);
        spawnManager?.StopSpawnManager();
        meteoManager?.StopSpawnMeteo();

        yield return new WaitForSeconds(3f);
        // 3초 기다리고 게임오버 팝업을 열 수 있음(추가 로직 필요)
    }
}
