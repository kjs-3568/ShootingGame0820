using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ����� �ӵ��� ���������� �̵��ϴ� ���
// �߻������ ��ü�� �ٸ� ���� ���� �ε�������, ���濡�� �������� �����ϴ� ���
public class Projetile : MonoBehaviour, Imovement
{
    [SerializeField]
    private float moveSpeed = 10f; // �̵��ӵ�
    private float damage; // ������
    private Vector2 moveDir; // �̵�����
    private GameObject owner; // �߻������ ��ü
    private string ownerTag; // ��ü�� �±�(���� ���� �����ϱ� ���ؼ�)

    private bool isInIt = false; // ������ ���õǾ������� �����ϵ����ϴ� Ʈ����


    // ����ü�� ����� �����ϱ� ���� ������ �������ִ� �ʱ�ȭ�Լ�
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
        Move(Vector2.zero); // Vector2.zero�� ���� ���� : Ư���� �ǹ̾���
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != owner && !collision.CompareTag(ownerTag))
        {
            // ���Ŀ� �� ��: ������ ����� �ο�
            Destroy(gameObject); // ���Ŀ� ������ƮǮ������ ����
        }
    }
}
