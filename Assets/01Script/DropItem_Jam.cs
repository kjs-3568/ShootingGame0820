using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾�� �浹üũ
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))] // Ŭ���� �ٷ� ���� �����ؾ���(���� x
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

            // ���� ����
            Vector2 initVelocity = Vector2.zero;
            initVelocity.x = Random.Range(-0.5f, 0.5f);
            initVelocity.y = Random.Range(3.0f, 4.0f);
            rig.AddForce(initVelocity, ForceMode2D.Impulse); // AddForce: ������ ���� ������ä�� ���� �߰�
                                                             // Impulse: ���� ���� 0���� ����� ���������� ���� �߰�
                                                             // >> �������� �� ������ ���Ⱑ��
        }
    }
    public void OnPickup(GameObject picker)
    {
        OnPickupJam?.Invoke();
        Destroy(gameObject); // ���Ŀ� ������ƮǮ�� ����
    }
}
