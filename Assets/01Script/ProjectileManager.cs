using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum projectileType
{
    plyer01,
    plyer02,
    plyer03,
    Boss01,
    Boss02,
    Boss03,
}
public class ProjectileManager : SingleTone<ProjectileManager>
{
    [SerializeField]
    private GameObject[] projectilePref;
    private Queue<Projetile>[] projectileQueue;

    private int poolSize = 10; // Ǯ�� ũ��. �ѹ��� �����ϴ� ������Ʈ ����

    protected override void Awake()
    {
        base.Awake();

        projectileQueue = new Queue<Projetile>[projectilePref.Length]; // ť�� �����ϴ� �迭�� 6�� ¥����.
        for(int i = 0; i < projectilePref.Length; i++)
        {
            projectileQueue[i] = new Queue<Projetile>(); // ������ ť�� ����
            Allocate((projectileType)i);
        }
    }

    GameObject obj;
    Projetile proj;

    private void Allocate(projectileType type) // ������Ÿ�� ������� �̸� �����صδ� ����
    {
        for(int i = 0; i < poolSize; i++)
        {
            obj = Instantiate(projectilePref[(int)type]); // ������ �ν��Ͻ� �����ϰ�,
            if(obj.TryGetComponent<Projetile>(out proj)) // ������ �ν��Ͻ����� ��ũ��Ʈ��ü ã�Ƽ�
            {
                projectileQueue[(int)type].Enqueue(proj); // �ش� ť�� �߰�.(Enqueue: ť�� �־��ִ� �Լ�)
            }
            obj.SetActive(false);
        }
    }

    public void FireProjectile(projectileType type, Vector3 spawnPos, Vector2 dir,
                               GameObject newOwner, int damage, float newSpeed) // ������ ��ġ�� ������Ÿ�� �����ϰ�, ���ư��� ����� �ӵ��� �������ִ� ����
    {
        proj = GetProjectileFromPool(type);
        if(proj != null)
        {
            proj.transform.position = spawnPos;
            proj.gameObject.SetActive(true);
            proj.InitProjectile(type, dir, newOwner, damage, newSpeed);
        }
    }

    private Projetile GetProjectileFromPool(projectileType type) // ����ϱ� ���ؼ� Ǯ���� �ϳ��� ������
                                                                 // ť�� ������Ʈ�� 0�����, �߰��� �����ؼ� ����
    {
        if (projectileQueue[(int)type].Count < 1)
            Allocate(type);

        return projectileQueue[(int)type].Dequeue(); // ť���� �ϳ� ������.
    }

    public void ReturnProjectileToPool(Projetile returnProj, projectileType type) // ����� �Ϸ�� �������� Ǯ�� ��ȯ
    {
        returnProj.gameObject.SetActive(false);
        projectileQueue[(int)type].Enqueue(returnProj);
    }
}
