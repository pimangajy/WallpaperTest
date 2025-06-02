using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using static BackgroundProgress;
using static Level_Button;
using static CharaterDataManager;
using System.Collections.Generic;

public class GetCharator : MonoBehaviour
{
    CreateCharator CreateCharator;
    CharatorData CharatorData;

    public Text type;
    public Text level;

    private Dictionary<string, bool> discoveredCharater = new Dictionary<string, bool>();
    private Dictionary<string, int> charaterCount = new Dictionary<string, int>();
    public CharaterType charaterType = CharaterType.Default;
    public int charatorLevel;
    [Serializable]
    public class CharatereData
    {
        public Dictionary<string, bool> discoveredCharater;
        public Dictionary<string, int> charaterCount;
        public CharaterType charaterType;
        public int charatorLevel;
    }

    private string filePath;

    private void Awake()
    {
        GetLevelType();

        filePath = Application.persistentDataPath + "/charaterSaveData.json";
        LoadData();
    }
    private void Start()
    {
        LoadData();
        GetLevelType();
    }

    public void GetLevelType()
    {
        type.text = "Type: " + charaterType.ToString();
        level.text = "Level: " + charatorLevel;
    }

    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                CharatereData saveData = JsonConvert.DeserializeObject<CharatereData>(json);

                discoveredCharater = saveData?.discoveredCharater ?? new Dictionary<string, bool>();
                charaterCount = saveData?.charaterCount ?? new Dictionary<string, int>();
                charaterType = saveData != null ? saveData.charaterType : CharaterType.Default;
                charatorLevel = saveData.charatorLevel;

                Debug.Log("ĳ���� ������ �ε� �Ϸ�!");
            }
            catch (Exception e)
            {
                Debug.LogError("������ �ε� ����: " + e.Message);
                discoveredCharater = new Dictionary<string, bool>();
            }
        }
        else
        {
            // ���� ���� �� �⺻�� ����
            discoveredCharater = new Dictionary<string, bool>();
            charatorLevel = 0;
            Debug.Log("���� ������ ���� �ʱ�ȭ��.");
        }
    }
}
