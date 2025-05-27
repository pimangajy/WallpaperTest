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
    public CharaterType characterType  = CharaterType.Default; // �ʱ� Ÿ��

    public GameObject ItemList;  // ������ â���� ǥ�õ� ������ ����Ʈ��
    public Text Lv;  //  ���� ȭ�鿡 ǥ���� ����

    private void Start()
    {
        if(itemhandlers != null)
        {
            itemhandlers = GetComponentsInChildrenOnly<ItemHandler>(ItemList);
        }
        else Debug.Log("������ ����Ʈ ����");
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

        if (character_Level > 3) // 3���� �ʰ� �� �ʱ�ȭ
        {
            characterType = CharaterType.Default;
            character_Level = 0;
            Debug.Log("�ִ� ���� �ʰ�! �⺻ Ÿ������ �ʱ�ȭ.");
            Item_Plus();
        }
        else if (character_Level == 1 && characterType == CharaterType.Default)
        {
            characterType = GetRandomCharacterType(); // 1�����̸� ���� Ÿ�� �Ҵ�
            Debug.Log($"���� 1�� �Ǿ� ���� Ÿ�� ����: {characterType}");
        }
        else
        {
            Debug.Log($"������!  ���� Ÿ��: {characterType}, {(int)characterType}, ����: {character_Level}");
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
                Debug.LogWarning($"�� �� ���� ���� ��: {character_Level}");
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
        // ������ �߰� Ȯ�� �ڵ�
        if (itemhandlers.Count == 0)
        {
            Debug.Log("�������� �����ϴ�.");
            return;
        }

        if (exp_Gauge.value < exp_Gauge.maxValue)
        {
            Debug.Log("����ġ ����");
            return;
        }

        LevelUp();

        // ������ ó��
        int interest = Interest_Gauge.value >= Interest_Gauge.maxValue ? 1 : 0;
        if (Interest_Gauge.value >= Interest_Gauge.maxValue)
        {
            Interest_Gauge.value = 0;
            PlayerPrefs.SetInt("interest", 1); // ������ ���� ����
        }
        else
        {
            PlayerPrefs.SetInt("interest", 0);
        }

        // ������ �ʱ�ȭ
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
            Debug.Log("�������� �����ϴ�.");
            return;
        }

        int ran = UnityEngine.Random.Range(0, itemhandlers.Count);

        // ������ �����۵��߿� �ϳ� ������
        ItemHandler itemHandler = itemhandlers[ran];

        // ������ī��Ʈ ����
        if (itemHandler != null)
        {
            itemHandler.Item_Plus();

            Debug.Log((ran+1) + " ���� ������ ŉ��");
        }
    }
}
