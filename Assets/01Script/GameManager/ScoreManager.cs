using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���ݿ� �ʿ��� ��ġ�� ���������� �����ϴ� �Ŵ���.
// �÷��̾� HP, �÷��̾� ��ȭ��, ������ ����, ������ ������ ���� ���
// �ش� ��ġ���� UI��� �ǽð� ������ �ǵ���, �̺�Ʈ �߻���Ű�� ����.

// ��������Ʈ => action���� Ȱ���ϱ� (���Ŀ�..)
public class ScoreManager : MonoBehaviour
{
    public delegate void ScoreChange(int score);
    public static event ScoreChange OnChangeScore; // ������������ �̺�Ʈ
    public static event ScoreChange OnChangeJamCount;
    public static event ScoreChange OnChangeHP;
    public static event ScoreChange OnChangeBomb;
    public static event ScoreChange OnChangePower; // �Ű����ڰ� ������, ��������Ʈ ü�� ������ ����� ����

    public static event Action<int> OnchangeHP2; // ��������Ʈ�� �����ʰ�, �Ű����� �̸����� �ٷ� �׼����� �� �� ����


    private int score; // ���ӿ��� �÷��̾ ������ ����. ���� óġ�ϰų� ������ ��������������.
    private int curHP; // �÷��̾� �� HP
    private int maxHP; // �÷��̾� �ִ� HP
    private int jamCount; // ���� ���� ����
    private int powerLevel;
    private int bombCount;

    public int CurHp => curHP;
    public int MaxHP => maxHP;
    public int JamCount => jamCount;
    public int PowerLevel => powerLevel;
    public int BombCount => bombCount;
    // getter�� �ܺο��� ���� ���� �ְ�

    // �� ���������� setter�� �̿��Ѵٸ�?
    private int SetScore // score�� ��ȭ�� ���� �� �ڵ����� �κ����̼� �߻�.
    {
        set
        {
            score = value;
            OnChangeScore?.Invoke(score);
        }
    }

    public void InitScoreSet() // �ʱⰪ ���ð� Invoke ����
    {
        score = 0;
        OnChangeScore?.Invoke(score);

        curHP = maxHP = 5;
        OnChangeHP?.Invoke(curHP);

        powerLevel = 1;

        jamCount = 0;
        OnChangeJamCount?.Invoke(jamCount);

        bombCount = 3;
        OnChangeBomb?.Invoke(bombCount);
    }

    private void OnEnable()
    {
        Enermy.onMonsterDied += HandleMonsterDied;
        DropItem_Jam.OnPickupJam += HandleJamPickup;
    }

    private void OnDisable()
    {
        Enermy.onMonsterDied -= HandleMonsterDied;
        DropItem_Jam.OnPickupJam -= HandleJamPickup;
    }

    private void HandleMonsterDied(Enermy enermyInfo)
    {
        Debug.Log("���� ���� Event�� ĳġ�߽��ϴ�." + enermyInfo.gameObject.name);
        // ���߿� ���� ��� �� �����ϴ� ���� ������ ���Ŀ�..
        SetScore = score + 10;
    }

    private void HandleJamPickup()
    {
        SetScore = score + 7;

        jamCount++;
        OnChangeJamCount?.Invoke(jamCount); // ���ھ�ó�� setter ���� ���� ����
    }

    public void IncreaseHP() // ü�� 1ȸ��
    {
        curHP++;
        if(curHP > maxHP)
        {
            curHP = maxHP;
        }
        OnChangeHP?.Invoke(curHP);
    }

    public void IncreaseBombCount()
    {
        bombCount++;
        OnChangeBomb?.Invoke(bombCount);
    }

    public void PowerUp()
    {
        powerLevel++;
        OnChangePower?.Invoke(powerLevel);
    }
}
