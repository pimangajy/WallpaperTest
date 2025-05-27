using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallpaperManager : MonoBehaviour
{
    private void Awake()
    {
        // 기본 설정
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // 해상도 설정
        SetScreenResolution();
    }

    private void SetScreenResolution()
    {
        // 화면 해상도에 맞춰 카메라 조정
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
