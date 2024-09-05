using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 진행 중 지속적으로 몬스터를 생성해 내는 매니저.
public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnTrans;
    [SerializeField]
    private GameObject[] spawnEnermyPrefabs;

    // 일반 몬스터 스폰이 완료되었다는 이벤트를 발생
    public delegate void SpawnFinish();
    public static event SpawnFinish OnSpawnFinish;

    private float spawnDelta = 1f;
    private int spawnLevel = 0;
    private int spawnCount = 0;

    private void Awake()
    {
        StartCoroutine(SpawnEnermys());
    }

    GameObject obj;
    IEnumerator SpawnEnermys()
    {
        yield return null;

        while(spawnCount <= 9)
        {
            for (int i = 0; i < spawnTrans.Length; i++)
            {
                obj = Instantiate(spawnEnermyPrefabs[spawnLevel], spawnTrans[i].position, Quaternion.identity);
                if (obj.TryGetComponent<Enermy>(out Enermy enermy))
                    enermy.SetEnabled(true);
            }
            spawnCount++;
            yield return new WaitForSeconds(spawnDelta);
        }
        spawnLevel++;
        if(spawnLevel >= 3)
        {
            spawnLevel = 0;
        }
        spawnCount = 0;
        OnSpawnFinish?.Invoke();
    }
}
