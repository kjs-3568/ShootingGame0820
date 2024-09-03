using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ƴٴϴ� �����۵��� ����� ����� �����ϱ����� �߻�Ŭ����.
// �߻�Ŭ����(abstract class) Ư¡:
// 1. ������ �ν��Ͻ� �����Ұ�. �Ļ�Ŭ����(�ڽ�)�� ����� ����.
// 2. ����� ��ɰ� �Ļ�Ŭ������ ��Ư�� ����� ����(�������� �����ϱ� ���� ���)
// 3. ���������� ��Ȯ�Ͽ� ���ϻ�ӱ����� ������ �� ���� �� ���.
public abstract class FlyItemBase : MonoBehaviour, Imovement, IPickuped
{
    public abstract void ApplyEffect(GameObject target); // �ٵ� ���� �߻� �޼ҵ�: �Ļ�Ŭ�������� �ݵ�� �����ؾ���

    private bool isInit = false;
    private float flySpeed = 0.7f;
    private Vector2 flyDirection = Vector2.zero;
    private Vector3 flyTargetPos;

    private void Awake()
    {
        SetEnabled(true);
    }

    private void Update()
    {
        if (isInit)
            Move(flyDirection);
    }

    public void Move(Vector2 direction)
    {
        transform.Translate(flyDirection * (flySpeed * Time.deltaTime)); 


    }
    public void SetEnabled(bool newEnable)
    {
        isInit = newEnable;

        if (isInit)
            StartCoroutine("ChangeFlyDirection");
        else
            StopCoroutine("ChangeFlyDirection");
    }

    public void OnPickup(GameObject picker)
    {
        throw new System.NotImplementedException();
    }

    // ������ �ǰ��� 4�ʿ� �ѹ��� ������ �������ִ� �ڷ�ƾ
    IEnumerator ChangeFlyDirection()
    {
        while(true)
        {
            flyTargetPos.x = Random.Range(-2f, 2f);
            flyTargetPos.y = Random.Range(-2f, 2f);
            flyTargetPos.z = 0f;

            flyDirection = (flyTargetPos - transform.position).normalized;
            yield return new WaitForSeconds(4f);
        }
    }
}
