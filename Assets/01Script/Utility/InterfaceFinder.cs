using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 월드에 있는 특정 인터페이스를 모두 탐색해오기.
// Interface: 순수 가상함수로만 이뤄진 추상클래스. 다중상속이 가능
public class InterfaceFinder : MonoBehaviour
{
    //FindObjectsOfType<> : FindObjectsByType<> 보다 옛날거. 기능이 좀 더 낙후됨. 그러나 유니티 버전에 따라 없을 수 잇음
    //FindObjectsByType<> : 기존 것 보다 좋은. 데이터 정렬기능이 있음. 뒤에 (FindObjectsSortMode.타입)을 적을 것
    public static List<T> FindObjectsOfInterface<T>() where T : class // public static이기 때문에 어디서든 쓸 수 있음
    {
        MonoBehaviour[] allObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        List<T> interfaceObjects = new List<T>();

        foreach (var obj in allObjects) // var : 확실한 타입이 아닌, 타입을 두리뭉실하게 가져올 수 있음
        {
            // if(obj is T) : obj가 T 타입의 객체인지 확인함. is는 캐스팅(형변환) 명령어
            if (obj is T interfaceObj) // obj를 T 타입으로 캐스팅해서 성공하면,interfaceObj에 참조한다.
            {
                interfaceObjects.Add(interfaceObj);
            }
        }
        return interfaceObjects;
    }
}
