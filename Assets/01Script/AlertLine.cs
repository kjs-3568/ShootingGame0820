using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertLine : MonoBehaviour
{
    [SerializeField]
    private GameObject meteoPrefab;

    private Animator anims;
    private Animator Anims
    {
        get
        { 
            if (anims == null)
            {
                anims = GetComponent<Animator>();
            }
            return anims;
        }
    }
    public void SpawnedLine()
    {
        // 2. �ִϸ��̼� ���� �� ���׿� ����
        // 1. �ִϸ��̼� ���

        Anims.SetTrigger("Spawn");
        Invoke("SpawnMeteo", 2f);
    }

    private void SpawnMeteo()
    {
        Vector3 spawnPos = transform.position;
        spawnPos.y += 8f;

        GameObject obj = Instantiate(meteoPrefab, spawnPos, Quaternion.identity);

        if(obj.TryGetComponent<Meteorite>(out Meteorite meteorite))
        {
            meteorite.InitMeteo();
            Destroy(gameObject);
        }
    }
}
