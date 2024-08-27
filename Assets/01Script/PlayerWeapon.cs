using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour, Iweapon
{
    [SerializeField]
    private GameObject projectilePrefab; // ����ü ����
    [SerializeField]
    private Transform firePoint; // ����ü �߻���ġ

    private int numpOfProjectiles = 1; // ����ü �߻�Ǵ� ����
    private float spreadAngled = 5; // ����ü�� ������ �߻� �� ��, ���� ����
    private float fireRate = 0.3f; // ����ü �߻簣��
    private float nextFireTime = 0f;

    private bool isFiring = false; // ���Ⱑ �߻� ������ �����ϴ� ����

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
            nextFireTime = Time.time + fireRate; // fireRate��ŭ �߻� Ÿ�̹��� ����

            //������Ÿ�� ����
            //������Ÿ���� �̵�����(�ʱ�ȭ)

            startAngle = -spreadAngled * (numpOfProjectiles - 1) / 2;

            for(int i = 0; i < numpOfProjectiles; i++)
            {
                angle = startAngle + spreadAngled * i;

                fireRotation = firePoint.rotation * Quaternion.Euler(0f, 0f, angle); // ����ü �߻����

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
