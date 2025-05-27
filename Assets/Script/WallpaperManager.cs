using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallpaperManager : MonoBehaviour
{
    private void Awake()
    {
        // �⺻ ����
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // �ػ� ����
        SetScreenResolution();
    }

    private void SetScreenResolution()
    {
        // ȭ�� �ػ󵵿� ���� ī�޶� ����
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            float screenHeight = Screen.height;
            float screenWidth = Screen.width;
            float targetAspect = screenWidth / screenHeight;
            mainCamera.aspect = targetAspect;
        }
    }
}
