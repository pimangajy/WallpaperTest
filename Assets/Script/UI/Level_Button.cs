using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using static Level_Button;

public class Level_Button : MonoBehaviour
{
    public Slider exp_Gauge;
    public Slider Interest_Gauge;

    public List<ItemHandler> itemhandlers = new List<ItemHandler>();

    public Gauge gauge;

    public Encyclopedia_Button encyclopedia;
    public CharaterType characterType  = CharaterType.Default; // 초기 타입

    public GameObject ItemList;  // 아이템 창에서 표시될 아이템 리스트들
    public Text Lv;  //  메인 화면에 표시할 레벨

    private void Start()
    {
        if(itemhandlers != null)
        {
            itemhandlers = GetComponentsInChildrenOnly<ItemHandler>(ItemList);
        }
        else Debug.Log("아이템 리스트 없음");
    }

    public static List<T> GetComponentsInChildrenOnly<T>(GameObject parentObject) where T : Component
    {
        List<T> result = new List<T>();

        foreach (Transform child in parentObject.transform)
        {
            T component = child.GetComponent<T>();
            if (component != null)
            {
                result.Add(component);
            }
        }

        return result;
    }
    public void LevelUp()
    {
        int character_Level = CharaterDataManager.Instance.charatorLevel;
        character_Level++;
        Lv.text = character_Level.ToString();

        if (character_Level > 3) // 3레벨 초과 시 초기화
        {
            characterType = CharaterType.Default;
            character_Level = 0;
            Debug.Log("최대 레벨 초과! 기본 타입으로 초기화.");
            Item_Plus();
        }
        else if (character_Level == 1 && characterType == CharaterType.Default)
        {
            characterType = GetRandomCharacterType(); // 1레벨이면 랜덤 타입 할당
            Debug.Log($"레벨 1이 되어 랜덤 타입 변경: {characterType}");
        }
        else
        {
            Debug.Log($"레벨업!  현재 타입: {characterType}, {(int)characterType}, 레벨: {character_Level}");
        }

        CharaterDataManager.Instance.charaterType = characterType;
        CharaterDataManager.Instance.charatorLevel = character_Level;
        CharaterDataManager.Instance.SaveData();
        CharaterDataManager.Instance.AddCharater(CharacterAssetManager.Instance.GetCharatorData(character_Level, characterType));

        switch (character_Level)
        {
            case 0:
                gauge.MaxGauge(3600);
                break;

            case 1:
                gauge.MaxGauge(7200);
                break;

            case 2:
                gauge.MaxGauge(10800);
                break;

            case 3:
                gauge.MaxGauge(18000);
                break;
            default:
                Debug.LogWarning($"알 수 없는 레벨 값: {character_Level}");
                break;
        }
    }
    private CharaterType GetRandomCharacterType()
    {
        CharaterType[] types = { CharaterType.yuniNomal};
        return types[UnityEngine.Random.Range(0, types.Length)];
    }
    public void ButtonClick()
    {
        // 아이템 추가 확인 코드
        if (itemhandlers.Count == 0)
        {
            Debug.Log("아이템이 없습니다.");
            return;
        }

        if (exp_Gauge.value < exp_Gauge.maxValue)
        {
            Debug.Log("경험치 부족");
            return;
        }

        LevelUp();

        // 애정도 처리
        int interest = Interest_Gauge.value >= Interest_Gauge.maxValue ? 1 : 0;
        if (Interest_Gauge.value >= Interest_Gauge.maxValue)
        {
            Interest_Gauge.value = 0;
            PlayerPrefs.SetInt("interest", 1); // 애정도 상태 저장
        }
        else
        {
            PlayerPrefs.SetInt("interest", 0);
        }

        // 게이지 초기화
        exp_Gauge.value = 0;
        if (interest == 1)
        {
            Interest_Gauge.value = 0;
        }
    }

    public void Item_Plus()
    {
        if (itemhandlers.Count == 0)
        {
            Debug.Log("아이템이 없습니다.");
            return;
        }

        int ran = UnityEngine.Random.Range(0, itemhandlers.Count);

        // 랜덤한 아이템들중에 하나 가져옴
        ItemHandler itemHandler = itemhandlers[ran];

        // 아이템카운트 증가
        if (itemHandler != null)
        {
            itemHandler.Item_Plus();

            Debug.Log((ran+1) + " 번쨰 아이템 흭득");
        }
    }
}
