using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageControllerr : MonoBehaviour
{
    public ScrollRect scrollRect;
    public int currentPage = 0;
    public int totalPages = 3;

    public void NextPage()
    {
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            MoveToPage(currentPage);
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            MoveToPage(currentPage);
        }
    }

    void MoveToPage(int pageIndex)
    {
        float target = (float)pageIndex / (totalPages - 1);
        StartCoroutine(SmoothScrollTo(target));
    }

    IEnumerator SmoothScrollTo(float target)
    {
        float duration = 0.3f;
        float time = 0;
        float start = scrollRect.horizontalNormalizedPosition;

        while (time < duration)
        {
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(start, target, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        scrollRect.horizontalNormalizedPosition = target;
    }
}
