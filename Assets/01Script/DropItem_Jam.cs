using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어와 충돌체크
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))] // 클래스 바로 위에 선언해야함(엔터 x
public class DropItem_Jam : MonoBehaviour,IPickuped
{
    public delegate void PickupJam();
    public static event PickupJam OnPickupJam;

    CircleCollider2D col;
    Rigidbody2D rig;

    private void Awake()
    {
        if(TryGetComponent<CircleCollider2D>(out col))
        {
            col.radius = 0.2f;
            col.isTrigger = true;
        }
        if(TryGetComponent<Rigidbody2D>(out rig))
        {
            rig.gravityScale = 1.0f;

            // 추후 수정
            Vector2 initVelocity = Vector2.zero;
            initVelocity.x = Random.Range(-0.5f, 0.5f);
            initVelocity.y = Random.Range(3.0f, 4.0f);
            rig.AddForce(initVelocity, ForceMode2D.Impulse); // AddForce: 기존의 힘을 유지한채로 힘을 추가
                                                             // Impulse: 기존 힘을 0으로 만들어 순간적으로 힘을 추가
                                                             // >> 아이템이 펑 터지는 연출가능
        }
    }
    public void OnPickup(GameObject picker)
    {
        OnPickupJam?.Invoke();
        Destroy(gameObject); // 추후에 오브젝트풀링 적용
    }
}
