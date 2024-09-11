using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponBase
{
    protected GameObject owner;
}
public class BossWeapons01 : BossWeaponBase, Iweapon
{
    Vector3 firePos;
    int numOfProj = 5;
    float spreadAngele = 15f;

    public void Fire()
    {
        firePos = owner.transform.position;

        for(int i = 0; i< numOfProj; i++)
        {
            float angle = spreadAngele * (i - (numOfProj - 1) / 2f);
            Vector2 fireDir = Quaternion.Euler(0, 0, angle) * Vector2.down;

            ProjectileManager.Inst.FireProjectile(projectileType.Boss01, firePos, fireDir, owner, 1, 6f);
        }
    }

    public void SetEnabled(bool enabled)
    {
        
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }
}

public class BossWeapons02 : BossWeaponBase, Iweapon
{
    public void Fire()
    {
        Vector3 firePos = owner.transform.position;

        int numOfProj = 36;
        float angleDelta = 360f / numOfProj;
        float startAngle = Random.Range(-10f, 10f);

        for (int i = 0; i < numOfProj; i++)
        {
            float spawnAngle = i * angleDelta + startAngle;
            Vector2 fireDir = Quaternion.Euler(0f, 0f, angleDelta) * Vector2.down;
            ProjectileManager.Inst.FireProjectile(projectileType.Boss02, firePos, fireDir, owner, 1, 2f);
        }
    }

    public void SetEnabled(bool enabled)
    {

    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }
}

public class BossWeapons03 : BossWeaponBase, Iweapon
{
    private float angleChangeValue = 6f;
    private float maxAngle = 45f;
    private float centerAngle = 0f;
    private int numOfProj = 5;

    public void Fire()
    {
        // 3번 무기의 공격방식 구현 (숙제)

        //while (true)
        //{
        //    Vector3 firePos = owner.transform.position;
        //    Vector2 fireDir = Quaternion.Euler(0f, 0f, curAngle) * Vector2.down;
        //    ProjectileManager.Inst.FireProjectile(projectileType.Boss03, firePos, fireDir, owner, 1, 6f);

        //    curAngle += angleChangeValue;
        //    if (curAngle > maxAngle || curAngle < (maxAngle * -1))
        //        angleChangeValue *= -1;
        //}

        Vector3 firePos = owner.transform.position;


        for (int i = 0; i < numOfProj; i++)
        {
            // 각 탄환의 각도를 부채꼴로 분배
            float angle = centerAngle + (maxAngle * (i - (numOfProj - 1) / 2f));


            // 각도에 따른 발사 방향 계산
            Vector2 fireDir = Quaternion.Euler(0f, 0f, centerAngle) * Vector2.down;
            ProjectileManager.Inst.FireProjectile(projectileType.Boss03, firePos, fireDir, owner, 1, 6f);

            // 중심각을 변경하여 다음 발사 시 탄환의 패턴이 변화
            centerAngle += angleChangeValue;

            // 중심각이 일정 범위를 넘으면 반대 방향으로 각도 변경
            if (centerAngle > maxAngle || centerAngle < -maxAngle)
            {
                angleChangeValue *= -1;
            }
        }
    }

    public void SetEnabled(bool enabled)
    {

    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }
}
