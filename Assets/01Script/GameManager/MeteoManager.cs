using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 경고 라인을 일정시간(2초~3초 사이 랜덤하게) 간격으로 스폰함
// 경고 라인 스폰 좌표(-2f~2f , 0 , 0)에서 생성
// 스폰의 반복은 GameManager에 의해서 꺼질수도, 켜질수도 있도록.

// +심화) 특수한 기능을 추가해서 UI의 새로운 값을 추가해보기
public class MeteoManager : MonoBehaviour
{
    [SerializeField]
    private GameObject alertLinePref;

    private float nextSpawnTime;
    private float spawnDelta = 3f;
    private GameObject obj;
    private AlertLine alertLine;
    private Vector3 spawnpos = Vector3.zero;
    private bool isSpawning = false;

    private void Start()
    {
        StartSpawnMeteo();
    }
    public void StartSpawnMeteo()
    {
        StartCoroutine("SpawnMeteo");
    }
    public void StopSpawnMeteo()
    {
        StopCoroutine("SpawnMeteo");
    }
    IEnumerator SpawnMeteo()
    {
        yield return null;

        while (true)
        {
            yield return new WaitForSeconds(spawnDelta);
            spawnpos.x = Random.Range(-2.2f, 2.2f);

            obj = Instantiate(alertLinePref, spawnpos, Quaternion.identity);
            if (obj.TryGetComponent<AlertLine>(out alertLine))
            {
                alertLine.SpawnedLine();
            }
        }
    }
    public void SetSpawnDelta(float newSpawnDelta) // 나중에 스폰시간을 변경하고 싶으면 호출하면 됨.
    {
        spawnDelta = Mathf.Clamp(newSpawnDelta, 0.5f, 3f);
    }

}
