using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enum: ������. 0���� �����ϴ� �ѹ��� �Űܼ� ������. �������� ����ٰ� ���� ��
public enum BossState 
{
    BS_MoveToAppear,// ������ġ�� �̵��ϴ� ����
    BS_Phase01,// ���ڸ����� ������ �ݺ��ϴ� ����
    BS_Phase02,// �¿�� �̵��ϸ鼭 ������ �ݺ��ϴ� ����
}

// AI�� �� �����ϰ� �ϰ� �ʹٸ� AI��ũ��Ʈ�� ��������ũ��Ʈ�� �и��ص� �ǰ�, �ൿƮ���� ����������
// ���� AI�� �ڷ�ƾ�� ���ؼ� SFM�� ����
public class BossAI : MonoBehaviour, Imovement, Idamaged
{
    [SerializeField]
    private float bossAppearPointY = 2.5f;
    private BossState bossState = BossState.BS_MoveToAppear; //������ ���� � �������� ������

    private Iweapon[] weapons;
    private Iweapon curWeapon;

    private Vector2 moveDir = Vector2.zero;
    private bool isInit = false;
    private float moveSpeed = 3f;
    private string bossName;
    private int maxHP;
    private int curHP;

    public bool isDead { get => curHP <= 0; }

    public delegate void BossDiedEvent();
    public event BossDiedEvent OnBossDied;

    public void InitBoss(string name, int newHP, Iweapon[] newWeapons)
    {
        // ui ����
        bossName = name;
        curHP = maxHP = newHP;
        
        weapons = newWeapons;
        SetEnabled(true);
        ChangeState(BossState.BS_MoveToAppear);
    }

    public void ChangeState(BossState newState) // ���¸� �������ִ� �޼ҵ�
    {
        StopCoroutine(bossState.ToString());
        bossState = newState;
        StartCoroutine(bossState.ToString());
    }

    private IEnumerator BS_MoveToAppear()
    {
        // �Ʒ�(Vector3.down ����)�� �̵��� ����.
        // ���� ��ġ�� �����߳�?
        // Y : ������� ����
        // N : ��� �̵��Ѵ�

        moveDir = Vector2.down;

        while(true)
        {
            if(transform.position.y <= bossAppearPointY) // ������ �ߴٸ�
            {
                moveDir = Vector2.zero; // �̵��� �����.
                ChangeState(BossState.BS_Phase01);
            }
            // �������� ���ߴٸ�,
            yield return null;
        }
    }

    private IEnumerator BS_Phase01()
    {
        // �� ���� Ȱ��ȭ
        curWeapon = weapons[0];
        curWeapon.SetEnabled(true);
            while(true)
            {
                curWeapon.Fire();
                yield return new WaitForSeconds(0.4f);
            }
    }

    private IEnumerator BS_Phase02()
    {
        // ���ⱳü (2��° ���Ͽ� ���� ����)
        curWeapon = weapons[1];

        // �¿�� �����ư��� �̵���
        moveDir = Vector2.right;

        while(true)
        {
            curWeapon.Fire();

            if(transform.position.x <= -2.5f || transform.position.x >= 2.5f)
            {
                moveDir *= -1f; // �������� ����
            }
            yield return new WaitForSeconds(0.5f); // 0.5�ʸ��� Ȯ��
        }
    }
    private void Update()
    {
        if (isInit)
            Move(moveDir);
    }


    public void Move(Vector2 direction)
    {
        transform.Translate(moveDir * (moveSpeed * Time.deltaTime));
    }

    public void SetEnabled(bool newEnable)
    {
        isInit = newEnable;
    }

    public void TakeDamage(GameObject attacker, int damage)
    {
        if (!isDead)
        {
            curHP -= damage;


            if (curHP > 0)
                OnDamaged();
            else
                OnDied();
        }
    }

    private void OnDamaged()
    {
        // �������� ���� �� ���� �� ó���ؾ��ϴ� ���� ������ ��Ƽ�.

        Debug.LogFormat("���� �޾Ҵ� ���� HP : {0}" , curHP);

        if(bossState == BossState.BS_Phase01 && (float)curHP/ maxHP < 0.5f) // HP�� 50�̸����� �������� �Ǹ�
        {
            ChangeState(BossState.BS_Phase02); // 2�� �������� ����
        }
    }

    private void OnDied() // HP�� 0�� �� ���� �� ó���ؾ��ϴ� ���� ������ ��Ƽ�.
    {
        OnBossDied?.Invoke(); //onMonsterDied�� ���� �ƴ� ��, �ڱ��ڽ��� ������ �Ѱ���

        Debug.Log("�׾���");
        Destroy(gameObject);
    }

    // ������ ó���ؾ���
}
