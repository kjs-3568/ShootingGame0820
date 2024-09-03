using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerItemCollector : MonoBehaviour
{
    private Rigidbody2D rig;
    private CircleCollider2D col;

    private void Awake()
    {
        if(TryGetComponent<Rigidbody2D>(out rig))
        {
            rig.gravityScale = 0.0f;
        }
        if(TryGetComponent<CircleCollider2D>(out col))
        {
            col.radius = 1.0f;
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            if(collision.TryGetComponent<IPickuped>(out IPickuped pickItem))
            {
                pickItem.OnPickup(transform.root.gameObject); // 계층구조적으로 부모인 오브젝트(Player)를 습득자로 불러오기 위해서
            }
        }
    }

}
