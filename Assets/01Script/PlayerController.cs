using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Iweapon currentWeapon;
    private Imovement movement;

    private void Awake()
    {
        currentWeapon = GetComponent<Iweapon>();
        movement = GetComponent<Imovement>();
    }

    public void CustomUpdate(Vector2 moveDir) // 게임매니저에서 업데이트를 관리하도록함
    {
        movement?.Move(moveDir);
        currentWeapon?.Fire();
    }
    //private void Update()
    //{
    //    currentWeapon?.Fire();
    //}

    public void StartGame()
    {
        currentWeapon?.SetEnabled(true);
        movement?.SetEnabled(true);
    }

    public void OverGame()
    {
        currentWeapon?.SetEnabled(false);
        movement?.SetEnabled(false);
    }
}
