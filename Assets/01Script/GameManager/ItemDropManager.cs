using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터가 사망하는 걸 캐치해서 아이템을 드랍하는 기능
public class ItemDropManager : MonoBehaviour
{
    [SerializeField]
    private GameObject jamPrefab;
    [SerializeField]
    private List<GameObject> flyItem;

    GameObject obj;

    private void OnEnable()
    {
        Enermy.onMonsterDied += HandleEnemyDie;
    }
    private void OnDisable()
    {
        Enermy.onMonsterDied -= HandleEnemyDie;
    }

    private int dropRate;

    private void HandleEnemyDie(Enermy enemyInfo)
    {
        for(int i = 0; i < 7; i++)
        {
            obj = Instantiate(jamPrefab, enemyInfo.transform.position, Quaternion.identity);
        }

        dropRate = Random.Range(0, 1000);
        if(dropRate < 10)
        {
            obj = Instantiate(flyItem[0], enemyInfo.transform.position, Quaternion.identity);
        }
        else if(dropRate < 20)
        {
            obj = Instantiate(flyItem[1], enemyInfo.transform.position, Quaternion.identity);
        }
        else if (dropRate < 500)
        {
            obj = Instantiate(flyItem[2], enemyInfo.transform.position, Quaternion.identity);
        }
    }    
}
