using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 피격 이벤트 발생
public class PlayerHitBox : MonoBehaviour, Idamaged
{
    public static Action<bool> OnPlayerHpUncreased; // true면 회복, false면 감소
    public void TakeDamage(GameObject attacker, int damage)
    {
        OnPlayerHpUncreased?.Invoke(false);
    }
}
