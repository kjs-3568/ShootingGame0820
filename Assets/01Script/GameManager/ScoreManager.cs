using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 전반에 필요한 수치를 통합적으로 관리하는 매니저.
// 플레이어 HP, 플레이어 강화도, 게임의 점수, 습득한 보석의 갯수 등등
// 해당 수치들이 UI등에서 실시간 갱신이 되도록, 이벤트 발생시키는 역할.

// 델리게이트 => action으로 활용하기 (추후에..)
public class ScoreManager : MonoBehaviour
{
    public delegate void ScoreChange(int score);
    public static event ScoreChange OnChangeScore; // 게임점수변경 이벤트
    public static event ScoreChange OnChangeJamCount;
    public static event ScoreChange OnChangeHP;
    public static event ScoreChange OnChangeBomb; // 매개인자가 같으면, 델리게이트 체인 여러개 만들기 가능


    private int score; // 게임에서 플레이어가 습득한 점수. 적을 처치하거나 보석을 습득했을때마다.
    private int curHP; // 플레이어 현 HP
    private int maxHP; // 플레이어 최대 HP
    private int jamCount; // 보석 습득 갯수
    private int powerLevel;
    private int bombCount;

    public int CurHp => curHP;
    public int MaxHP => maxHP;
    public int JamCount => jamCount;
    public int PowerLevel => powerLevel;
    public int BombCount => bombCount;
    // getter로 외부에서 읽을 수만 있게

    // 더 상위버전인 setter를 이용한다면?
    private int SetScore // score가 변화가 생길 때 자동으로 인보케이션 발생.
    {
        set
        {
            score = value;
            OnChangeScore?.Invoke(score);
        }
    }

    public void InitScoreSet() // 초기값 세팅과 Invoke 세팅
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
        Debug.Log("몬스터 죽음 Event를 캐치했습니다." + enermyInfo.gameObject.name);
        // 나중에 몬스터 사망 시 습득하는 게임 점수는 추후에..
        SetScore = score + 10;
    }

    private void HandleJamPickup()
    {
        SetScore = score + 7;

        jamCount++;
        OnChangeJamCount?.Invoke(jamCount); // 스코어처럼 setter 만들 수도 있음
    }
}
