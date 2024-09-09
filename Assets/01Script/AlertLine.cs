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
        // 2. 애니메이션 종료 후 메테오 스폰
        // 1. 애니메이션 재생

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
