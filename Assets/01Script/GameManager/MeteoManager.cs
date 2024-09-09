using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ������ �����ð�(2��~3�� ���� �����ϰ�) �������� ������
// ��� ���� ���� ��ǥ(-2f~2f , 0 , 0)���� ����
// ������ �ݺ��� GameManager�� ���ؼ� ��������, �������� �ֵ���.

// +��ȭ) Ư���� ����� �߰��ؼ� UI�� ���ο� ���� �߰��غ���
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
    public void SetSpawnDelta(float newSpawnDelta) // ���߿� �����ð��� �����ϰ� ������ ȣ���ϸ� ��.
    {
        spawnDelta = Mathf.Clamp(newSpawnDelta, 0.5f, 3f);
    }

}
