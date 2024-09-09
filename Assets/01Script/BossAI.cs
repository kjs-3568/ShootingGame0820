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
public class BossAI : MonoBehaviour, Imovement
{
    [SerializeField]
    private float bossAppearPointY = 2.5f;
    private BossState bossState = BossState.BS_MoveToAppear; //������ ���� � �������� ������

    private Iweapon[] weapons;
    private Iweapon curWeapon;

    public void InitBoss(string name, int newHP, Iweapon[] newWeapons)
    {
        // ui ����
        weapons = newWeapons;
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

        Move(Vector3.down);

        while(true)
        {
            if(transform.position.y <= bossAppearPointY) // ������ �ߴٸ�
            {
                Move(Vector3.zero); // �̵��� �����
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

        while(true)
        {
            curWeapon = weapons[0];
            curWeapon.SetEnabled(true);
            while(true)
            {
                curWeapon.Fire();
                yield return null;
            }
        }
    }

    private IEnumerator BS_Phase02()
    {
        // ���ⱳü (2��° ���Ͽ� ���� ����)
        // �¿�� �����ư��� �̵���

        Vector2 dir = Vector2.right;
        Move(dir);

        while(true)
        {
            curWeapon.Fire();

            if(transform.position.x <= -2.5f || transform.position.z >= 2.5f)
            {
                dir *= -1f; // �������� ������Ŵ
                Move(dir);
            }
            yield return new WaitForSeconds(0.5f); // 0.5�ʸ��� Ȯ��
        }
    }



    public void Move(Vector2 direction)
    {
        
    }

    public void SetEnabled(bool newEnable)
    {
        
    }
}
