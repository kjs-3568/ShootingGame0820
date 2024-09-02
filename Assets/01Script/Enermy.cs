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


    public delegate void MonsterDiedEvent(Enermy enermyInfo); // �׾��ٴ� ������ �޴� ��������Ʈ
    public static event MonsterDiedEvent onMonsterDied;

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

    private void OnDied() // HP�� 0�� �� ���� �� ó���ؾ��ϴ� ���� ������ ��Ƽ�.
    {
        onMonsterDied?.Invoke(this); //onMonsterDied�� ���� �ƴ� ��, �ڱ��ڽ��� ������ �Ѱ���

        Debug.Log("�׾���");
        Destroy(gameObject);
    }
}
