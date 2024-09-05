using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� �� ���������� ���͸� ������ ���� �Ŵ���.
public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnTrans;
    [SerializeField]
    private GameObject[] spawnEnermyPrefabs;

    // �Ϲ� ���� ������ �Ϸ�Ǿ��ٴ� �̺�Ʈ�� �߻�
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
