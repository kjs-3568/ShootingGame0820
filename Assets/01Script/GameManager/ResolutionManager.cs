using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 해상도 관리자(여러 환경에서 플레이 할 수 있도록)
// 플레이하는 디바이스의 해상도와, 제작할 때 기준이 된 해상도가 서로 다를 때.
// 레터박스와 세터박스의 기법을 활용해서 제작한 비율이 유지가 되도록.
public class ResolutionManager : SingleTone<ResolutionManager>
{
    private Camera mainCam;
    private Canvas canvas;
    private CanvasScaler canvasScalier;

    private Vector2 fixedAspectRatio = new Vector2(9, 16);

    protected override void DoAwake() 
    {
        base.DoAwake(); // 부모클래스의 내용을 한번 실행 후 본인의 DoAwake를 호출
        ApplySetting();
    }

    private void ApplySetting()
    {
        if(mainCam == null)
        {
            mainCam = Camera.main;
        }
        if(canvas == null)
        {
            canvas = FindObjectOfType<Canvas>();
        }
        if(canvasScalier == null)
        {
            canvasScalier = FindObjectOfType<CanvasScaler>();
        }

        if (mainCam != null)
        {
            SetCameraAspectRatio(); // 해상도 고정
        }
        if (canvas != null && canvasScalier != null)
        {
            ConfigureCanvas();
        }
    }
    private void SetCameraAspectRatio()
    {
        Rect rt = mainCam.rect;

        float screenAspect = (float)(Screen.width / Screen.height); // 플레이 화면 가로세로 비율구하기
        float targetAspect = fixedAspectRatio.x / fixedAspectRatio.y; // 확정된 해상도

        if(screenAspect >= targetAspect) // 넓이가 크다면
        {
            float width = targetAspect / screenAspect;
            rt.width = width;
            rt.height = 1f;
            rt.x = (1f - width) / 2f;
            rt.y = 0f;
        }
        else
        {
            float height =  screenAspect / targetAspect;
            rt.width = 1f;
            rt.height = height;
            rt.x = 0f;
            rt.y = (1f - height) / 2f;
        }
        mainCam.rect = rt;
    }

    private void ConfigureCanvas()
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = mainCam;
        canvas.planeDistance = 1f; // 카메라의 바로 눈앞에 화면을 만듬

        canvasScalier.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScalier.referenceResolution = new Vector2(1080, 1920);
        canvasScalier.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        canvasScalier.matchWidthOrHeight = 0.5f;
    }
}
