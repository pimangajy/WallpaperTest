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
