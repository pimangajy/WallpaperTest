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
            Debug.Log($"{charater.name} 습득! 도감에 등록되었습니다.");
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
        Debug.Log(charater.name + " 캐릭터 " + charaterCount[charater.name] + " 번째 흭득");
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
            Debug.LogError("데이터 저장 오류: " + e.Message);
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

                Debug.Log("캐릭터 데이터 로드 완료!");
            }
            catch (Exception e)
            {
                Debug.LogError("데이터 로드 오류: " + e.Message);
                discoveredCharater = new Dictionary<string, bool>();
            }
        }
        else
        {
            // 최초 실행 시 기본값 설정
            discoveredCharater = new Dictionary<string, bool>();
            charatorLevel = 0;
            Debug.Log("저장 파일이 없어 초기화됨.");
        }
    }
    public void DeleteSaveFile()
    {
        discoveredCharater = new Dictionary<string, bool>();
        charaterCount = new Dictionary<string, int>();
        charatorLevel = 0;
        charaterType = CharaterType.Default;

        SaveData(); // 초기값으로 덮어쓰기
        Debug.Log("캐릭터 데이터가 초기화되었습니다.");
    }
}
