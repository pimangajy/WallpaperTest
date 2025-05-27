using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Click : MonoBehaviour
{
    public Rect clickableRect = new Rect(0.25f, 0.25f, 0.5f, 0.5f);
    public UnityEngine.Color gizmoColor = UnityEngine.Color.red;
    public Camera mainCamera;

    private bool isDragging = false;
    private Vector3 lastDragPosition;
    private float dragThreshold = 0.01f; // 드래그로 인식할 최소 이동 거리
    private float touchStartTime;
    private Vector3 touchStartPosition;
    private const float TOUCH_TIME_THRESHOLD = 0.2f; // 터치로 인식할 최대 시간 (초)


    [SerializeField]
    private List<Idle_Anime> idle_Animes = new List<Idle_Anime>();

    public Idle_Anime idle_Anime;

    public Imoticon_On_Off imoticon_1;
    public Imoticon_On_Off imoticon_2;
    public Imoticon_On_Off imoticon_3;
    public Imoticon_On_Off imoticon_4;

    int touchCount = 0;

    void Awake()
    {
        if (mainCamera == null)  // 카메라할당헀는지 확인
        {
            mainCamera = Camera.main;
            Debug.LogWarning("카메라가 할당되지 않아 메인 카메라를 사용합니다. Inspector에서 카메라를 직접 할당하는 것을 권장합니다.");
        }
    }

    private void Start()
    {

    }

    void Update()
    {
        if (mainCamera == null) return;

#if UNITY_EDITOR   // 유니티 에디터 마우스 입력 처리
        HandleMouseInput();
#elif UNITY_ANDROID  // 안드로이드 터치 입력 처리
        HandleTouchInput();
#endif
    }

    public void CharaotrIn(Idle_Anime obj)
    {
        idle_Anime = obj;
    }

    private void HandleMouseInput()
    {
        Vector3 viewportPoint = mainCamera.ScreenToViewportPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (clickableRect.Contains(viewportPoint))
            {
                touchStartTime = Time.time;
                touchStartPosition = viewportPoint;
                lastDragPosition = viewportPoint;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (!isDragging && clickableRect.Contains(viewportPoint))
            {
                float distance = Vector3.Distance(viewportPoint, touchStartPosition);
                if (distance > dragThreshold)
                {
                    isDragging = true;

                    idle_Anime.HeadIdle_On();

                    foreach (Idle_Anime idle in idle_Animes)
                    {
                        idle.HeadIdle_On();
                    }

                    imoticon_4.Imoticon_On();

                    Debug.Log("드래그 시작!");
                }
            }

            if (isDragging)
            {
                Vector3 delta = viewportPoint - lastDragPosition;
                //idle_Anime.DragEvent(viewportPoint, delta);
                lastDragPosition = viewportPoint;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                isDragging = false;

                foreach (Idle_Anime idle in idle_Animes)
                {
                    idle.HeadIdle_Off();
                }

                imoticon_4.Imoticon_Off();
                idle_Anime.HeadIdle_Off();

                Debug.Log("드래그 종료!");

                if(Time.time - touchStartTime > 2.0f)
                {
                    imoticon_1.Surprise_On_Off(0.5f);
                }

            }
            else if (Time.time - touchStartTime < TOUCH_TIME_THRESHOLD)
            {
                imoticon_4.Imoticon_Off();

                if(touchCount > 3)
                {
                    imoticon_2.Surprise_On_Off(0.5f);
                    touchCount = 0;
                }else
                {
                    touchCount++;
                    imoticon_3.Surprise_On_Off(0.3f);
                }

                Debug.Log("터치 이벤트 실행!");
            }
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 viewportPoint = mainCamera.ScreenToViewportPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (clickableRect.Contains(viewportPoint))
                    {
                        touchStartTime = Time.time;
                        touchStartPosition = viewportPoint;
                        lastDragPosition = viewportPoint;
                    }
                    break;

                case TouchPhase.Moved:
                    if (clickableRect.Contains(viewportPoint))
                    {
                        if (!isDragging)
                        {
                            float distance = Vector3.Distance(viewportPoint, touchStartPosition);
                            if (distance > dragThreshold)
                            {
                                isDragging = true;
                                Debug.Log("드래그 시작!");
                            }
                        }

                        if (isDragging)
                        {
                            Vector3 delta = viewportPoint - lastDragPosition;
                            //idle_Anime.DragEvent(viewportPoint, delta);
                            lastDragPosition = viewportPoint;
                        }
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (isDragging && clickableRect.Contains(viewportPoint))
                    {
                        isDragging = false;
                        //idle_Anime.DragEndEvent(viewportPoint);
                        Debug.Log("드래그 종료!");
                    }
                    else if (clickableRect.Contains(viewportPoint) &&
                           Time.time - touchStartTime < TOUCH_TIME_THRESHOLD)
                    {
                        Debug.Log("터치 이벤트 실행!");
                    }
                    break;
            }
        }
    }

    // 기존의 OnDrawGizmos 메서드는 그대로 유지
    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        // Rect를 화면 비율에 맞게 변환하여 그립니다
        // Recr에서 그린 사각형의 각 꼭짓점의 Vector위치를 저장
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(clickableRect.xMin, clickableRect.yMin, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(clickableRect.xMax, clickableRect.yMax, mainCamera.nearClipPlane));
        Vector3 topLeft = mainCamera.ViewportToWorldPoint(new Vector3(clickableRect.xMin, clickableRect.yMax, mainCamera.nearClipPlane));
        Vector3 bottomRight = mainCamera.ViewportToWorldPoint(new Vector3(clickableRect.xMax, clickableRect.yMin, mainCamera.nearClipPlane));

        // Gizmos로 사각형 그리기
        // Vector에 저장한 위치들을 서로 이어줌으로 사각형을 그림
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
    }
}