using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PanelHandler : MonoBehaviour
{
    void Start()
    {
        DOTween.Init();
        // transform 의 scale 값을 모두 0.1f로 변경합니다.
        transform.localScale = Vector3.one * 0.1f;
        // 객체를 비활성화 합니다.
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        UIManager.Instance.OpenUI(gameObject);

        // DOTween의 시퀀스를 생성
        var seq = DOTween.Sequence();

        // DOScale 의 첫 번째 파라미터는 목표 Scale 값, 두 번째는 시간입니다.

        seq.Append(transform.DOScale(1f, 0.2f));

        seq.Play();
    }

    public void Hide()
    {
        var seq = DOTween.Sequence();

        seq.Append(transform.DOScale(0.1f, 0.2f));

        // OnComplete 는 seq 에 설정한 애니메이션의 플레이가 완료되면
        // { } 안에 있는 코드가 수행된다는 의미입니다.
        // 여기서는 닫기 애니메이션이 완료된 후 객첼르 비활성화 합니다.
        seq.Play().OnComplete(() =>
        {
            gameObject.SetActive(false);
            UIManager.Instance.CloseUI();
        });
    }
}
