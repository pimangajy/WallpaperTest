using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

public class CreateCharator : MonoBehaviour
{
    public CharatorList CharatorList;
    public Click click;

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
        filePath = Application.persistentDataPath + "/charaterSaveData.json";
        LoadData();
    }

    void Start()
    {
        CharaterSpawn();
    }

    public void CharaterSpawn()
    {
        LoadData();

        CharatorData charData = CharatorList.GetCharatorData(charatorLevel, charaterType);

        if (charData != null)
        {
            GameObject spawnchar = Instantiate(charData.Charator, transform.position, Quaternion.identity);
            Debug.Log($"ĳ���� ���� �Ϸ�: {charData.CharatorName} ���� : {charatorLevel} Ÿ�� : {charaterType}");

            if (click != null && spawnchar.GetComponent<Idle_Anime>())
            {
                click.CharaotrIn(spawnchar.GetComponent<Idle_Anime>());
            }
            else Debug.Log("Click ��ũ��Ʈ Ȥ�� Idle_Anime�� �����ϴ�");
        }
        else
        {
            Debug.LogWarning("�ش� ���ǿ� �´� ĳ���� �����͸� ã�� �� �����ϴ�.");
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
}
