using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �ػ� ������(���� ȯ�濡�� �÷��� �� �� �ֵ���)
// �÷����ϴ� ����̽��� �ػ󵵿�, ������ �� ������ �� �ػ󵵰� ���� �ٸ� ��.
// ���͹ڽ��� ���͹ڽ��� ����� Ȱ���ؼ� ������ ������ ������ �ǵ���.
public class ResolutionManager : SingleTone<ResolutionManager>
{
    private Camera mainCam;
    private Canvas canvas;
    private CanvasScaler canvasScalier;

    private Vector2 fixedAspectRatio = new Vector2(9, 16);

    protected override void DoAwake() 
    {
        base.DoAwake(); // �θ�Ŭ������ ������ �ѹ� ���� �� ������ DoAwake�� ȣ��
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
            SetCameraAspectRatio(); // �ػ� ����
        }
        if (canvas != null && canvasScalier != null)
        {
            ConfigureCanvas();
        }
    }
    private void SetCameraAspectRatio()
    {
        Rect rt = mainCam.rect;

        float screenAspect = (float)(Screen.width / Screen.height); // �÷��� ȭ�� ���μ��� �������ϱ�
        float targetAspect = fixedAspectRatio.x / fixedAspectRatio.y; // Ȯ���� �ػ�

        if(screenAspect >= targetAspect) // ���̰� ũ�ٸ�
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
        canvas.planeDistance = 1f; // ī�޶��� �ٷ� ���տ� ȭ���� ����

        canvasScalier.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScalier.referenceResolution = new Vector2(1080, 1920);
        canvasScalier.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        canvasScalier.matchWidthOrHeight = 0.5f;
    }
}
