using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    List<IBackGroundScroller> backGroundScrollers;
    
    [SerializeField]
    private float scrollSpeed = 0f; // 인스펙터 창에서 관리

    private void Start()
    {
        backGroundScrollers = InterfaceFinder.FindObjectsOfInterface<IBackGroundScroller>();
    }

    private void Update()
    {
        foreach(var scroller in backGroundScrollers)
        {
            if (scroller != null)
                scroller.Scroll(Time.deltaTime);
        }
    }

    public void SetScrollSpeed(float newSpeed)
    {
        foreach(var scroller in backGroundScrollers)
        {
            if (scroller is IBackGroundScroller verticalScroller)
                verticalScroller.SetScrollSpeed(newSpeed);
        }
    }
}
