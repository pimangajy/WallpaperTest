using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemManager : MonoBehaviour
{
    public Item[] items;             // 아이템 목록
    public GameObject popupPanel;    // 팝업 창
    public Text itemName;            // 아이템 이름
    public Text popupText;           // 팝업 창의 텍스트
    public Button useButton;         // '예' 버튼
    public Button backButton;        // '아니오' 버튼
    public Text[] itemCountTexts;    // UI에 표시되는 아이템 개수 텍스트

    private int selectedItemIndex;   // 현재 선택된 아이템의 인덱스

    void Start()
    {

        DOTween.Init();
        // transform 의 scale 값을 모두 0.1f로 변경합니다.
        transform.localScale = Vector3.one * 0.1f;

        popupPanel.SetActive(false); // 팝업 창 비활성화
        UpdateUI();                  // UI 초기화
    }

    public void SelectItem(int index)
    {
        selectedItemIndex = index;

        // 아이템 개수 확인
        if (items[selectedItemIndex].itemCount > 0)
        {
            popupPanel.SetActive(true);
            popupText.text = $"{items[selectedItemIndex].itemName}을(를) 사용하시겠습니까?";
        }
        else
        {
            Debug.Log($"{items[selectedItemIndex].itemName}이(가) 없습니다.");
        }
    }

    public void UseItem()
    {
        if (items[selectedItemIndex].itemCount > 0)
        {
            items[selectedItemIndex].itemCount--; // 아이템 개수 감소

            // 아이템 기능 실행
            ExecuteItemFunction(items[selectedItemIndex].itemName);

            // 아이템 개수 업데이트
            UpdateUI();

            // 팝업 창 닫기
            popupPanel.SetActive(false);
        }
    }

    public void CancelUse()
    {
        popupPanel.SetActive(false);
    }

    private void UpdateUI()
    {
        for (int i = 0; i < items.Length; i++)
        {
            itemCountTexts[i].text = items[i].itemCount.ToString();
        }
    }

    private void ExecuteItemFunction(string itemName)
    {
        switch (itemName)
        {
            case "Health Potion":
                Debug.Log("체력을 회복했습니다!");
                // 체력 회복 로직 추가
                break;

            case "Mana Potion":
                Debug.Log("마나를 회복했습니다!");
                // 마나 회복 로직 추가
                break;

            default:
                Debug.Log("알 수 없는 아이템입니다.");
                break;
        }
    }
}
