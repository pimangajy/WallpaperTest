using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Open_UI : MonoBehaviour
{
    public RectTransform panel; // 창 역할의 RectTransform
    public Vector2 startSize = new Vector2(100, 100); // 시작 크기
    public Vector2 targetSize = new Vector2(500, 500); // 목표 크기
    public float animationDuration = 1.0f; // 애니메이션 지속 시간

    public Transform[] Children;

    void Start()
    {
        // 창 크기를 시작 크기로 초기화
        panel = gameObject.GetComponent<RectTransform>();
        panel.sizeDelta = startSize;

        for(int i = 0; i < transform.childCount; i++)
        {
            Children[i] = transform.GetChild(i);
        }
    }

    public void OpenWindow()
    {
        StartCoroutine(OpenAnimateWindow());
    }
    public void CloseWindow()
    {
        StartCoroutine(CloseAnimateWindow());
    }

    private IEnumerator OpenAnimateWindow()
    {
        float elapsedTime = 0;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;

            // 선형 보간(Lerp)을 통해 크기 점진적으로 변경
            panel.sizeDelta = Vector2.Lerp(startSize, targetSize, elapsedTime / animationDuration);

            yield return null; // 다음 프레임까지 대기
        }

        // 최종 크기 보정
        panel.sizeDelta = targetSize;
    }


    private IEnumerator CloseAnimateWindow()
    {
        float elapsedTime = 0;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;

            // 선형 보간(Lerp)을 통해 크기 점진적으로 변경
            panel.sizeDelta = Vector2.Lerp(targetSize, startSize, elapsedTime / animationDuration);

            yield return null; // 다음 프레임까지 대기
        }

        // 최종 크기 보정
        panel.sizeDelta = startSize;
    }
}
