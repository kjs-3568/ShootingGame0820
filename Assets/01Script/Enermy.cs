using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. ��ġ�� �Ǿ �÷��̾� ����
// 2. �̵����
// 3. ������ �޴� ���
// 4. ������ ������ ���

public class Enermy : MonoBehaviour, Imovement, Idamaged
{
    private Vector2 moveDir;
    private float moveSpeed = 3f;
    private bool isInit = false;

    // �� ü���� �ۼ�Ʈ�� �����ϱ� ����.
    private int curHp = 3;
    private int maxHp;

    public bool isDead { get => curHp <= 0; } // => ���ٽ�.( return ~~ )�� ����. <=�� �׳� ��

    private void Update() // ���� ����
    {
        Move(Vector2.down);
    }

    public void Move(Vector2 direction)
    {
        if (isInit)
            transform.Translate(direction * (moveSpeed * Time.deltaTime));
    }

    public void SetEnabled(bool newEnable)
    {
        isInit = newEnable;
    }

    public void TakeDamage(GameObject attacker, int damage)
    {
        if (!isDead)
        {
            curHp -= damage;


            if (curHp > 0)
                OnDamaged();
            else
                OnDied();
        }
    }

    private void OnDamaged()
    {
        // �������� ���� �� ���� �� ó���ؾ��ϴ� ���� ������ ��Ƽ�.

        Debug.LogFormat("���� �޾Ҵ� ���� HP : {0}, maxHp :  {1}, moveSpeed : {2}", curHp, maxHp, moveSpeed);
    }

    private void OnDied()
    {
        // HP�� 0�� �� ���� �� ó���ؾ��ϴ� ���� ������ ��Ƽ�.

        Debug.Log("�׾���");
        Destroy(gameObject);
    }

    Button bt;

    private void Awake()
    {
        bt = FindAnyObjectByType<Button>();

        bt.BP;
    }

    private void HandleBP(string color)
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        gameObject.GetComponent<Renderer>().material.color = randomColor;
    }
}
