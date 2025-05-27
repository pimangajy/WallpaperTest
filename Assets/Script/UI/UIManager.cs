using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instans;

    public Slider expSlider;
    public Slider interestSlider;
    private Dictionary<string, float> slideValue = new Dictionary<string, float>();

    private string filePath;

    public static UIManager Instance
    { 
        get 
        { 
            if(instans == null)
            {
                return null;
            }
            return instans;
        } 
    }

    private Stack<GameObject> openedUI = new Stack<GameObject>();

    private void Awake()
    {
        if (instans == null)
        {
            instans = this;

            DontDestroyOnLoad(this.gameObject);
        }

        filePath = Application.persistentDataPath + "/valuesaveData.json";
        LoadData();
    }
    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void OpenUI(GameObject uiObject)
    {
        openedUI.Push(uiObject);
    }

    public void CloseUI()
    {
        if (openedUI.Count > 0)
        {
            GameObject closedUI = openedUI.Pop();
        }
    }

    public void SlideSaveData(ItemData itemData)
    {
        if(slideValue.ContainsKey("expSlider") || slideValue.ContainsKey("interestSilder"))
        {
            slideValue["expSlider"] = 0;
            slideValue["interestSilder"] = 0;
        }

        if(slideValue.ContainsKey("expSlider") && itemData.itemType == ItemData.ItemType.expIncrease)
        {
            slideValue["expSlider"] += itemData.value;
        }
        else if(slideValue.ContainsKey("interestSilder") && itemData.itemType == ItemData.ItemType.interestIncrease)
        {
            slideValue["interestSilder"] += itemData.value;
        }
   
        SaveData();
    }

    public void SaveData()
    {
        try
        {
            var saveData = new { slideValue };
            string json = JsonConvert.SerializeObject(saveData, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        catch (Exception e)
        {
            Debug.LogError("����ġ ������ ���� ����: " + e.Message);
        }
    }

    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                var saveData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(json);

                // Ű ���� ���� üũ
                if (saveData.ContainsKey("slideValue"))
                    slideValue = saveData["slideValue"].ToDictionary(kv => kv.Key, kv => Convert.ToSingle(kv.Value));
                else
                    slideValue = new Dictionary<string, float>();

            }
            catch (Exception e)
            {
                Debug.LogError("������ �ε� ����: " + e.Message);
                slideValue = new Dictionary<string, float>();
            }
        }
        else
        {
            // ���� ���� �� �⺻�� ����
            slideValue = new Dictionary<string, float>();
            Debug.Log("���� ������ ���� �ʱ�ȭ��.");
        }
    }

}
