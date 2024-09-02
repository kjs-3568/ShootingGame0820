using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몬스터가 사망하는 걸 캐치해서 아이템을 드랍하는 기능
public class ItemDropManager : MonoBehaviour
{
    [SerializeField]
    private GameObject jamPrefab;

    GameObject obj;

    private void OnEnable()
    {
        Enermy.onMonsterDied += HandleEnemyDie;
    }
    private void OnDisable()
    {
        Enermy.onMonsterDied -= HandleEnemyDie;
    }

    private void HandleEnemyDie(Enermy enemyInfo)
    {
        for(int i = 0; i < 7; i++)
        {
            obj = Instantiate(jamPrefab, enemyInfo.transform.position, Quaternion.identity);
        }
    }    
}
