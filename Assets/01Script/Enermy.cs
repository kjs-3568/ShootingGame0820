using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 배치가 되어서 플레이어 방해
// 2. 이동기능
// 3. 데미지 받는 기능
// 4. 죽으면 리워드 기능

public class Enermy : MonoBehaviour, Imovement, Idamaged
{
    private Vector2 moveDir;
    private float moveSpeed = 3f;
    private bool isInit = false;

    // 현 체력의 퍼센트를 관리하기 위해.
    private int curHp;
    private int maxHp;

    public bool isDead { get => curHp <= 0; } // => 람다식.( return ~~ )와 같음. <=는 그냥 비교

    private void Update() // 추후 수정
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
        // 데미지를 받을 때 연출 등 처리해야하는 여러 로직을 모아서.
    }

    private void OnDied()
    {
        // HP가 0일 때 연출 등 처리해야하는 여러 로직을 모아서.
    }
}
