using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour, Iweapon
{
    [SerializeField]
    private GameObject projectilePrefab; // 투사체 종류
    [SerializeField]
    private Transform firePoint; // 투사체 발사위치

    private int numpOfProjectiles = 1; // 투사체 발사되는 갯수
    private float spreadAngled = 5; // 투사체가 여러발 발사 될 때, 사이 간격
    private float fireRate = 0.3f; // 투사체 발사간격
    private float nextFireTime = 0f;

    private bool isFiring = false; // 무기가 발사 중인지 관리하는 변수

    private void Update()
    {
        Fire();
    }

    float startAngle;
    float angle;
    Quaternion fireRotation;
    GameObject obj;
    Projetile projetileComp;

    public void Fire()
    {
        if(Time.time < nextFireTime)
            return;

        if (isFiring)
        {
            nextFireTime = Time.time + fireRate; // fireRate만큼 발사 타이밍을 조절

            //프로젝타일 생성
            //프로젝타일의 이동방향(초기화)

            startAngle = -spreadAngled * (numpOfProjectiles - 1) / 2;

            for(int i = 0; i < numpOfProjectiles; i++)
            {
                angle = startAngle + spreadAngled * i;

                fireRotation = firePoint.rotation * Quaternion.Euler(0f, 0f, angle); // 투사체 발사방향

                obj = Instantiate(projectilePrefab, firePoint.position, fireRotation);
                projetileComp = obj?.GetComponent<Projetile>();
                projetileComp?.InitProjectile(obj.transform.up, gameObject, 1f, 10f);
            }
        }
    }

    public void SetEnabled(bool enabled)
    {
        isFiring = enabled;
    }
}
