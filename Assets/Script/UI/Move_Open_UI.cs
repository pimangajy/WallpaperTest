using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Open_UI : MonoBehaviour
{
    public RectTransform panel; // â ������ RectTransform
    public Vector2 startSize = new Vector2(100, 100); // ���� ũ��
    public Vector2 targetSize = new Vector2(500, 500); // ��ǥ ũ��
    public float animationDuration = 1.0f; // �ִϸ��̼� ���� �ð�

    public Transform[] Children;

    void Start()
    {
        // â ũ�⸦ ���� ũ��� �ʱ�ȭ
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

            // ���� ����(Lerp)�� ���� ũ�� ���������� ����
            panel.sizeDelta = Vector2.Lerp(startSize, targetSize, elapsedTime / animationDuration);

            yield return null; // ���� �����ӱ��� ���
        }

        // ���� ũ�� ����
        panel.sizeDelta = targetSize;
    }


    private IEnumerator CloseAnimateWindow()
    {
        float elapsedTime = 0;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;

            // ���� ����(Lerp)�� ���� ũ�� ���������� ����
            panel.sizeDelta = Vector2.Lerp(targetSize, startSize, elapsedTime / animationDuration);

            yield return null; // ���� �����ӱ��� ���
        }

        // ���� ũ�� ����
        panel.sizeDelta = startSize;
    }
}
