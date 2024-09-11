using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enum: 열거형. 0부터 시작하는 넘버를 매겨서 관리함. 선택지를 만든다고 보면 됨
public enum BossState 
{
    BS_MoveToAppear,// 전투위치로 이동하는 상태
    BS_Phase01,// 제자리에서 공격을 반복하는 상태
    BS_Phase02,// 좌우로 이동하면서 공격을 반복하는 상태
}

// AI를 더 복잡하게 하고 싶다면 AI스크립트와 데미지스크립트를 분리해도 되고, 행동트리로 구현가능함
// 보스 AI는 코루틴을 통해서 SFM을 구현
public class BossAI : MonoBehaviour, Imovement, Idamaged
{
    [SerializeField]
    private float bossAppearPointY = 2.5f;
    private BossState bossState = BossState.BS_MoveToAppear; //보스가 현재 어떤 상태인지 관리함

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
        // ui 변경
        bossName = name;
        curHP = maxHP = newHP;
        
        weapons = newWeapons;
        SetEnabled(true);
        ChangeState(BossState.BS_MoveToAppear);
    }

    public void ChangeState(BossState newState) // 상태를 변경해주는 메소드
    {
        StopCoroutine(bossState.ToString());
        bossState = newState;
        StartCoroutine(bossState.ToString());
    }

    private IEnumerator BS_MoveToAppear()
    {
        // 아래(Vector3.down 방향)로 이동을 시작.
        // 전투 위치에 도달했나?
        // Y : 전투모드 돌입
        // N : 계속 이동한다

        moveDir = Vector2.down;

        while(true)
        {
            if(transform.position.y <= bossAppearPointY) // 도달을 했다면
            {
                moveDir = Vector2.zero; // 이동을 멈춘다.
                ChangeState(BossState.BS_Phase01);
            }
            // 도달하지 못했다면,
            yield return null;
        }
    }

    private IEnumerator BS_Phase01()
    {
        // 현 무기 활성화
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
        // 무기교체 (2번째 패턴에 맞춰 변경)
        curWeapon = weapons[1];

        // 좌우로 번갈아가며 이동함
        moveDir = Vector2.right;

        while(true)
        {
            curWeapon.Fire();

            if(transform.position.x <= -2.5f || transform.position.x >= 2.5f)
            {
                moveDir *= -1f; // 움직임을 반전
            }
            yield return new WaitForSeconds(0.5f); // 0.5초마다 확인
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
        // 데미지를 받을 때 연출 등 처리해야하는 여러 로직을 모아서.

        Debug.LogFormat("공격 받았다 남은 HP : {0}" , curHP);

        if(bossState == BossState.BS_Phase01 && (float)curHP/ maxHP < 0.5f) // HP가 50미만으로 떨어지게 되면
        {
            ChangeState(BossState.BS_Phase02); // 2번 패턴으로 변경
        }
    }

    private void OnDied() // HP가 0일 때 연출 등 처리해야하는 여러 로직을 모아서.
    {
        OnBossDied?.Invoke(); //onMonsterDied가 널이 아닐 때, 자기자신의 정보를 넘겨줌

        Debug.Log("죽었다");
        Destroy(gameObject);
    }

    // 데미지 처리해야함
}
