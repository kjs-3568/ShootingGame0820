using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���Ͱ� ����ϴ� �� ĳġ�ؼ� �������� ����ϴ� ���
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
