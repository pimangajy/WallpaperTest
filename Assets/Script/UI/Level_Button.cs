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
        Lv.text = character_Level.ToString();

        CharaterDataManager.Instance.charaterType = characterType;
        CharaterDataManager.Instance.charatorLevel = character_Level;
        CharaterDataManager.Instance.SaveData();
        CharaterDataManager.Instance.AddCharater(CharacterAssetManager.Instance.GetCharatorData(character_Level, characterType));
        BackgroundProgress.Instance.LevelUp();
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
