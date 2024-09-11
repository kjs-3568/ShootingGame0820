using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �������� �帧�� ����.
// 1. ������ ����, ������ ����, ������ ����.
// 2. ���� ������
// 3. �����ε� : ���� �ε� ��,rom�����͸� Ram�������� �ҷ����� �۾�
// 4. �� ���� : �� ����ÿ� �����͸� �ְ�ް�, ��������Ǹ� ���� �����Ű�� ��.
// 5. (�ӽ�) �Է��� �޾Ƽ� ĳ���Ϳ� ����.(IinputHandler�κ��� ���������� ���޹޾Ƽ�, ĳ���� movement�� ����)

public class GameManager : SingleTone<GameManager> // �̱����� ��ӹ���
{
    ScrollManager scrollManager;

    private IinputHandler inputHandler;
    private PlayerController pc;
    private MeteoManager meteoManager;
    private SpawnManager spawnManager;
    private ScoreManager scoreManager;

    GameObject obj; // �޼ҵ� ���ݿ��� ������ ã����.

    private void Start()
    {
        scrollManager = GameObject.FindAnyObjectByType<ScrollManager>();

        StartCoroutine(GameStart());

        LoadSceneInit(); // (�ӽ�) ���߿� �� ������ �� �� ȣ��ǵ��Ϻ����� ����
    }

    private void LoadSceneInit() // ���� ������ �� ��, ����� UI���� joystick ã�ƿ���
                                 // ���� ���� ����Ǵ� Player��ü�� ã�Ƽ� ����.
    {
        inputHandler = GetComponent<InputKeybord>(); // ���߰����� �ӽ��ڵ�

        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null)
            Debug.Log("GameManager.cs - LoadSceneInit() - pc ��������!");

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


    IEnumerator GameStart() // ���ӽ��� �� ������ ���� �����̸� ��.
    {
        yield return null;
        Debug.Log("���� ������ �ʱ�ȭ");
        scoreManager?.InitScoreSet();
        scoreManager.OnDiedPlayer += PlayerDiedProcess;
        yield return new WaitForSeconds(2f);
        pc?.StartGame(); // ���ӽ����ϸ� ������ �����ϰ�, ������ �Է� �޾ƿ�
        Debug.Log("�÷��̾� ��Ʈ�� On");
        yield return new WaitForSeconds(1f);
        scrollManager?.SetScrollSpeed(4f);
        Debug.Log("��� ��ũ�� ����");
        yield return new WaitForSeconds(2f);
        spawnManager?.InitSpawnManager();
        Debug.Log("���� ���� ����");
        yield return new WaitForSeconds(5f);
        meteoManager.StartSpawnMeteo();
        Debug.Log("���׿� ���� ����");
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
        // 3�� ��ٸ��� ���ӿ��� �˾��� �� �� ����(�߰� ���� �ʿ�)
    }
}
