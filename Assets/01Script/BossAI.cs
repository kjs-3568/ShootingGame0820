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
public class BossAI : MonoBehaviour, Imovement
{
    [SerializeField]
    private float bossAppearPointY = 2.5f;
    private BossState bossState = BossState.BS_MoveToAppear; //보스가 현재 어떤 상태인지 관리함

    private Iweapon[] weapons;
    private Iweapon curWeapon;

    public void InitBoss(string name, int newHP, Iweapon[] newWeapons)
    {
        // ui 변경
        weapons = newWeapons;
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

        Move(Vector3.down);

        while(true)
        {
            if(transform.position.y <= bossAppearPointY) // 도달을 했다면
            {
                Move(Vector3.zero); // 이동을 멈춘다
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
        // 무기교체 (2번째 패턴에 맞춰 변경)
        // 좌우로 번갈아가며 이동함

        Vector2 dir = Vector2.right;
        Move(dir);

        while(true)
        {
            curWeapon.Fire();

            if(transform.position.x <= -2.5f || transform.position.z >= 2.5f)
            {
                dir *= -1f; // 움직임을 반전시킴
                Move(dir);
            }
            yield return new WaitForSeconds(0.5f); // 0.5초마다 확인
        }
    }



    public void Move(Vector2 direction)
    {
        
    }

    public void SetEnabled(bool newEnable)
    {
        
    }
}
