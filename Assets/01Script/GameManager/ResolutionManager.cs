using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// �ػ� ������. 
// �÷����ϴ� ����̽��� �ػ󵵿�, �����Ҷ� ������ �� �ػ󵵰� ���� �ٸ���,
// ���͹ڽ��� ���͹ڽ��� ����� Ȱ���ؼ� ������ ������ ������ �ǵ���. 
public class ResolutionManager : SingleTone<ResolutionManager>
{
    private Camera mainCam;
    private Canvas canvas;
    private CanvasScaler canvasScalier;

    private Vector2 fixedAspectRatio = new Vector2(9, 16);

    protected override void DoAwake()
    {
        base.DoAwake(); // �θ�Ŭ������ ������ �ѹ� ���� �Ŀ� ������ doAwake�� ȣ��.

        // �ʱ⼳�� 
        ApplySetting();
    }

    private void ApplySetting()
    {
        if (mainCam == null)
        {
            mainCam = Camera.main;
        }

        if (canvas == null)
        {
            canvas = FindObjectOfType<Canvas>();
        }

        if (canvasScalier == null)
            canvasScalier = FindObjectOfType<CanvasScaler>();


        if (mainCam != null)
        {
            //�ػ� ����
            SetCameraAspectRatio();
        }

        if (canvas != null && canvasScalier != null)
        {
            ConfigureCanvas();
        }
    }

    private void SetCameraAspectRatio()
    {
        // ī�޶��� �簢�� (rect) ������ ������
        Rect rt = mainCam.rect;

        // ���� ȭ���� ��Ⱦ�� ���
        float screenAspect = (float)Screen.width / Screen.height;

        // ������ ��Ⱦ�� ���
        float targetAspect = fixedAspectRatio.x / fixedAspectRatio.y;

        // ���� ȭ�� ��Ⱦ�� ��ǥ ��Ⱦ�񺸴� ũ�ų� ������
        if (screenAspect >= targetAspect)
        {
            // ȭ���� �ʺ� �����Ͽ� ��ǥ ��Ⱦ�� ����
            float width = targetAspect / screenAspect;
            rt.width = width;
            rt.height = 1f;
            rt.x = (1f - width) / 2f; // �¿� ���� ����
            rt.y = 0f; // ���� ���� ����
        }
        else
        {
            // ȭ���� ���̸� �����Ͽ� ��ǥ ��Ⱦ�� ����
            float height = screenAspect / targetAspect;
            rt.width = 1f;
            rt.height = height;
            rt.x = 0f; // �¿� ���� ����
            rt.y = (1f - height) / 2f; // ���� ���� ����
        }

        // ī�޶��� �簢�� (rect) ���� ����
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
