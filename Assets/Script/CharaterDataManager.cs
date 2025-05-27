using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Linq;
using static ItemDataManager;
using UnityEngine.TextCore.Text;
using static UnityEditor.Progress;

public class CharaterDataManager : MonoBehaviour
{
    private static CharaterDataManager _instance;
    public static CharaterDataManager Instance { get; private set; }

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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        filePath = Application.persistentDataPath + "/charaterSaveData.json";
        LoadData();
    }

    public bool IsCharaterDiscovered(CharatorData charater)
    {
        return discoveredCharater.TryGetValue(charater.name, out bool isDiscovered) && isDiscovered;
    }


    public void DiscoverCharater(CharatorData charater)
    {
        if (!discoveredCharater.ContainsKey(charater.name))
        {
            discoveredCharater[charater.name] = true;
            Debug.Log($"{charater.name} ����! ������ ��ϵǾ����ϴ�.");
        }
    }

    public void AddCharater(CharatorData charater)
    {
        if(!charaterCount.ContainsKey(charater.name))
        {
            charaterCount[charater.name] = 0;
        }
        DiscoverCharater(charater);
        charaterCount[charater.name] ++;
        Debug.Log(charater.name + " ĳ���� " + charaterCount[charater.name] + " ��° ŉ��");
        SaveData();
    }

    public void SaveData()
    {
        try
        {
            CharatereData saveData = new CharatereData
            {
                discoveredCharater = this.discoveredCharater,
                charaterCount = this.charaterCount,
                charatorLevel = this.charatorLevel,
                charaterType = this.charaterType
            };
            string json = JsonConvert.SerializeObject(saveData, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
            Debug.Log(charaterType + " / " + charatorLevel);
        }
        catch (Exception e)
        {
            Debug.LogError("������ ���� ����: " + e.Message);
        }
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
    public void DeleteSaveFile()
    {
        discoveredCharater = new Dictionary<string, bool>();
        charaterCount = new Dictionary<string, int>();
        charatorLevel = 0;
        charaterType = CharaterType.Default;

        SaveData(); // �ʱⰪ���� �����
        Debug.Log("ĳ���� �����Ͱ� �ʱ�ȭ�Ǿ����ϴ�.");
    }
}
