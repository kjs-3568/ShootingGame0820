using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� �ǰ� �̺�Ʈ �߻�
public class PlayerHitBox : MonoBehaviour, Idamaged
{
    public static Action<bool> OnPlayerHpUncreased; // true�� ȸ��, false�� ����
    public void TakeDamage(GameObject attacker, int damage)
    {
        OnPlayerHpUncreased?.Invoke(false);
    }
}
