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

    private int poolSize = 10; // 풀의 크기. 한번에 생성하는 오브젝트 갯수

    protected override void Awake()
    {
        base.Awake();

        projectileQueue = new Queue<Projetile>[projectilePref.Length]; // 큐를 관리하는 배열을 6개 짜리로.
        for(int i = 0; i < projectilePref.Length; i++)
        {
            projectileQueue[i] = new Queue<Projetile>(); // 각각의 큐를 생성
            Allocate((projectileType)i);
        }
    }

    GameObject obj;
    Projetile proj;

    private void Allocate(projectileType type) // 프로젝타일 사용전에 미리 생성해두는 로직
    {
        for(int i = 0; i < poolSize; i++)
        {
            obj = Instantiate(projectilePref[(int)type]); // 프리펩 인스턴스 생성하고,
            if(obj.TryGetComponent<Projetile>(out proj)) // 생성된 인스턴스에서 스크립트객체 찾아서
            {
                projectileQueue[(int)type].Enqueue(proj); // 해당 큐에 추가.(Enqueue: 큐에 넣어주는 함수)
            }
            obj.SetActive(false);
        }
    }

    public void FireProjectile(projectileType type, Vector3 spawnPos, Vector2 dir,
                               GameObject newOwner, int damage, float newSpeed) // 지정된 위치에 프로젝타일 생성하고, 날아가는 방향과 속도를 설정해주는 역할
    {
        proj = GetProjectileFromPool(type);
        if(proj != null)
        {
            proj.transform.position = spawnPos;
            proj.gameObject.SetActive(true);
            proj.InitProjectile(type, dir, newOwner, damage, newSpeed);
        }
    }

    private Projetile GetProjectileFromPool(projectileType type) // 사용하기 위해서 풀에서 하나씩 꺼내옴
                                                                 // 큐애 오브젝트가 0개라면, 추가로 생성해서 리턴
    {
        if (projectileQueue[(int)type].Count < 1)
            Allocate(type);

        return projectileQueue[(int)type].Dequeue(); // 큐에서 하나 꺼내옴.
    }

    public void ReturnProjectileToPool(Projetile returnProj, projectileType type) // 사용이 완료된 프로젝을 풀에 반환
    {
        returnProj.gameObject.SetActive(false);
        projectileQueue[(int)type].Enqueue(returnProj);
    }
}
