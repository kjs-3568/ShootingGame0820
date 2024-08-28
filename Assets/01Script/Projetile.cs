using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 지정된 방향과 속도로 지속적으로 이동하는 기능
// 발사시켜준 주체와 다른 팀의 대상과 부딪혔을때, 상대방에게 데미지를 전달하는 기능
public class Projetile : MonoBehaviour, Imovement
{
    [SerializeField]
    private float moveSpeed = 10f; // 이동속도
    private float damage; // 데미지
    private Vector2 moveDir; // 이동방향
    private GameObject owner; // 발사시켜준 주체
    private string ownerTag; // 주체의 태그(상대방 팀을 구분하기 위해서)

    private bool isInIt = false; // 정보가 세팅되었을때만 동작하도록하는 트리거


    // 투사체의 기능을 수행하기 위해 정보를 세팅해주는 초기화함수
    public void InitProjectile(Vector2 newDir, GameObject newOwner, float newDamage, float newSpeed)
    {
        moveDir = newDir;
        damage = newDamage;
        moveSpeed = newSpeed;

        owner = newOwner;
        ownerTag = owner.tag;

        SetEnabled(true);
    }

    public void Move(Vector2 direction)
    {
        if (isInIt)
        {
            transform.Translate(moveDir * (moveSpeed * Time.deltaTime));
        }
    }

    public void SetEnabled(bool newEnable)
    {
        isInIt = newEnable;
    }

    private void Update()
    {
        Move(Vector2.zero); // Vector2.zero를 넣은 이유 : 특별히 의미없음
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != owner && !collision.CompareTag(ownerTag))
        {
            // 추후에 할 일: 적에게 대미지 부여
            Destroy(gameObject); // 추후에 오브젝트풀링으로 수정
        }
    }
}
