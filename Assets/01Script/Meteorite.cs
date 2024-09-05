using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Meteorite : MonoBehaviour
{
    private Rigidbody2D rig;
    private Rigidbody2D Rig
    {
        get
        {
            if(rig == null)
            {
                rig = GetComponent<Rigidbody2D>();
            }
            return rig;
        }
    }
    private CircleCollider2D col;
    private CircleCollider2D Col
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
    private void Awake()
    {
        Col.isTrigger = true;
        Col.radius = 0.3f;
        Rig.gravityScale = 1f;
    }
    

    public void InitMeteo() // 메테오가 게임 내에 도달하면, 메테오의 속도를 초기화
    {
        Rig.velocity = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(collision.TryGetComponent<Idamaged>(out Idamaged damage))
            {
                damage.TakeDamage(gameObject, 1);
                Destroy(gameObject);
            }
        }
    }
}
