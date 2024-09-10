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
    public void Fire()
    {
        // 3번 무기의 공격방식 구현 (숙제)
    }

    public void SetEnabled(bool enabled)
    {

    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }
}
