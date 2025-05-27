using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Move_UI : MonoBehaviour
{
    // 출발 위치
    public Vector2 start_point;
    // 도착 위치
    public Vector2 end_point;

    private RectTransform rectTransform;

    // 이동 시간
    public float duration = 1.0f;

    void Awake()
    {
        // RectTransform 컴포넌트 가져오기
        rectTransform = GetComponent<RectTransform>();
    }


    // 코루틴을 실행하기 위한 함수
    public void Open()
    {
        gameObject.SetActive(true);
        StartCoroutine(Open_Panel());
        UIManager.Instance.OpenUI(gameObject);
    }
    public void Close()
    {
        StartCoroutine(Close_Panel());
        UIManager.Instance.CloseUI();
    }

    public IEnumerator Open_Panel()
    {
        float elapsedTime = 0;

        // 출발 위치로 초기화
        rectTransform.anchoredPosition = start_point;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // 선형 보간으로 이동 (출발위치, 도착 위치, 시간)
            rectTransform.anchoredPosition = Vector3.Lerp(start_point, end_point, t);

            yield return null;
        }

        // 정확한 위치로 설정
        rectTransform.anchoredPosition = end_point;
    }

    public IEnumerator Close_Panel()
    {
        float elapsedTime = 0;
        rectTransform.anchoredPosition = end_point;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // 선형 보간으로 이동
            rectTransform.anchoredPosition = Vector3.Lerp(end_point, start_point, t);

            yield return null;
        }

        // 정확한 위치로 설정
        rectTransform.anchoredPosition = start_point;
        gameObject.SetActive(false);
    }
}
