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
    private Imovement movementController;

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

        obj = FindObjectsByType<PlayerMove>(FindObjectsSortMode.None)[0].gameObject; // �ӽ��ڵ�
        if (obj != null)
        {
            if (!obj.TryGetComponent<Imovement>(out movementController))
                Debug.Log("GameManager.cs - LoadSceneInit() - movementController ��������!");
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


    IEnumerator GameStart() // ���ӽ��� �� ������ ���� �����̸� ��.
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("���� �غ�");
        yield return new WaitForSeconds(2f);
        scrollManager?.SetScrollSpeed(4f); // ?. : null üũ ������. if(n != null)�� ���� ����.
    }
}
