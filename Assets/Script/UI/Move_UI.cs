using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Move_UI : MonoBehaviour
{
    // ��� ��ġ
    public Vector2 start_point;
    // ���� ��ġ
    public Vector2 end_point;

    private RectTransform rectTransform;

    // �̵� �ð�
    public float duration = 1.0f;

    void Awake()
    {
        // RectTransform ������Ʈ ��������
        rectTransform = GetComponent<RectTransform>();
    }


    // �ڷ�ƾ�� �����ϱ� ���� �Լ�
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

        // ��� ��ġ�� �ʱ�ȭ
        rectTransform.anchoredPosition = start_point;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // ���� �������� �̵� (�����ġ, ���� ��ġ, �ð�)
            rectTransform.anchoredPosition = Vector3.Lerp(start_point, end_point, t);

            yield return null;
        }

        // ��Ȯ�� ��ġ�� ����
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

            // ���� �������� �̵�
            rectTransform.anchoredPosition = Vector3.Lerp(end_point, start_point, t);

            yield return null;
        }

        // ��Ȯ�� ��ġ�� ����
        rectTransform.anchoredPosition = start_point;
        gameObject.SetActive(false);
    }
}
