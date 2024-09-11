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
    [SerializeField]
    private GameObject[] spawnBossPrefabs;

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
    BossAI bossAI;
    IEnumerator SpawnEnermys()
    {
        yield return null;

        while(spawnCount <= 3)
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
        OnSpawnFinish?.Invoke(); // �Ϲݸ��� ������ ���ᰡ �Ǹ�,
        // ���� �����̶�� ���޼����� 3�ʰ� ����,
        // ������ �����Ŵ

        yield return new WaitForSeconds(3f);

        obj = Instantiate(spawnBossPrefabs[spawnLevel], new Vector3(0f, 8f, 0f), Quaternion.identity);

        if(obj.TryGetComponent<BossAI>(out bossAI))
        {
            Iweapon[] weapons = new Iweapon[] { new BossWeapons03(), new BossWeapons01() };
            foreach(var weapon in weapons)
            {
                weapon?.SetOwner(obj);
            }
            bossAI.InitBoss("���������� ����", 500, weapons);
            bossAI.OnBossDied += NextLevel;

            //weapons = new Iweapon[] { new BossWeapons02(), new BossWeapons03() };
            //bossAI.InitBoss("��û�� ����", 1000, weapons);

            //weapons = new Iweapon[] { new BossWeapons01(), new BossWeapons02() };
            //bossAI.InitBoss("������ ����", 1000, weapons);
        }

        spawnLevel++;
        if(spawnLevel >= 3)
        {
            spawnLevel = 0;
        }
        spawnCount = 0;
        
    }

    public void NextLevel()
    {
        bossAI.OnBossDied -= NextLevel;
        StartCoroutine(SpawnEnermys());
    }
}
