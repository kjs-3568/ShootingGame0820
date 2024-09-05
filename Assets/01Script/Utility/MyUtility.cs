using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUtility : MonoBehaviour
{
    public static Transform FindChildRecursive(Transform parent, string targetName)
    {
        foreach(Transform child in parent)
        {
            if(child.name == targetName)
            {
                return child;
            }
            Transform findTrans = FindChildRecursive(child, targetName); // 재귀함수를 통해 깊이우선탐색으로 자식의 자식도 찾도록.
            if (findTrans != null)
            {
                return findTrans;
            }
        }
        return null;
    }
}
