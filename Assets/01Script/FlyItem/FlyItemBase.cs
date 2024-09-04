using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 날아다니는 아이템들의 공통된 기능을 구현하기위한 추상클래스.
// 추상클래스(abstract class) 특징:
// 1. 스스로 인스턴스 생성불가. 파생클래스(자식)을 만드는 목적.
// 2. 공통된 기능과 파생클래스의 독특한 기능을 구현(다형성을 구현하기 위한 요소)
// 3. 계층구조가 명확하여 단일상속구조를 유지할 수 있을 때 사용.
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public abstract class FlyItemBase : MonoBehaviour, Imovement, IPickuped
{
    public abstract void ApplyEffect(GameObject target); // 바디가 없는 추상 메소드: 파생클래스에서 반드시 구현해야함

    private bool isInit = false;
    private float flySpeed = 0.7f;
    private Vector2 flyDirection = Vector2.zero;
    private Vector3 flyTargetPos;

    private Rigidbody2D rig;
    private CircleCollider2D col;
    private CircleCollider2D Col // 게터를 통해 참조를 새로 하는 방식
    {
        get
        {
            if(col == null)
            {
                col = GetComponent<CircleCollider2D>();
            }
            return col;
        }
    }

    private ScoreManager scoreManager;

    protected ScoreManager ScoreMgr
    {
        get
        {
            if (scoreManager == null)
            {
                scoreManager = FindAnyObjectByType<ScoreManager>();
            }
            return scoreManager;
        }
    }

    private void Awake()
    {
        if(TryGetComponent<Rigidbody2D>(out rig))
        {
            rig.gravityScale = 0f;
        }
        Col.isTrigger = true;
        Col.radius = 0.23f;

        SetEnabled(true);
    }

    private void Update()
    {
        if (isInit)
            Move(flyDirection);
    }

    public void Move(Vector2 direction)
    {
        transform.Translate(flyDirection * (flySpeed * Time.deltaTime)); 


    }
    public void SetEnabled(bool newEnable)
    {
        isInit = newEnable;

        if (isInit)
            StartCoroutine("ChangeFlyDirection");
        else
            StopCoroutine("ChangeFlyDirection");
    }

    public void OnPickup(GameObject picker)
    {
        ApplyEffect(picker);
    }

    // 스폰이 되고나서 4초에 한번씩 방향을 설정해주는 코루틴
    IEnumerator ChangeFlyDirection()
    {
        while(true)
        {
            flyTargetPos.x = Random.Range(-2f, 2f);
            flyTargetPos.y = Random.Range(-2f, 2f);
            flyTargetPos.z = 0f;

            flyDirection = (flyTargetPos - transform.position).normalized;
            yield return new WaitForSeconds(4f);
        }
    }
}
