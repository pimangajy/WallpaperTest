using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class BackgroundProgress : MonoBehaviour
{
    private static BackgroundProgress _instance;
    public static BackgroundProgress Instance { get; private set; }
    private string filePath;

    public Slider exp_Gauge;
    public Slider Interest_Gauge;
    public Text Lv;
    public Text In;
    Dictionary<int, float> levelExpMap = new Dictionary<int, float>
    {
        { 0, 3600f },
        { 1, 7200f },
        { 2, 10800f },
        { 3, 18000f }
    };

    public int exp;
    public int interest;
    public string lastExitTime;
    public TimeSpan timeElapsed;

    [Serializable]
    public class PointSaveData
    {
        public int exp;
        public int interest;
        public string lastExitTime;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        filePath = Application.persistentDataPath + "/pointSaveData.json";
        LoadData();
    }

    void Start()
    {
        if (!string.IsNullOrEmpty(lastExitTime))
        {
            float elapsedSeconds = (float)timeElapsed.TotalSeconds;

            exp_Gauge.value = Mathf.Clamp(exp_Gauge.value + elapsedSeconds, exp_Gauge.minValue, exp_Gauge.maxValue);
            exp = (int)exp_Gauge.value;

            // 애정도 감소
            float lostInterest = elapsedSeconds * (10f / 3600f);  // 시간당 10 감소
            Interest_Gauge.value = Mathf.Clamp(Interest_Gauge.value - lostInterest, Interest_Gauge.minValue, Interest_Gauge.maxValue);
            interest = (int)Interest_Gauge.value;

            SaveData();  // 갱신된 값 저장
        }
    }

    public void LevelUp()
    {
        exp = 0;
        exp_Gauge.value = exp;
        interest = (int)Interest_Gauge.value;
        Interest_Gauge.value = interest;

        if(Interest_Gauge.value >= Interest_Gauge.maxValue)
        {
            interest = 0;
            Interest_Gauge.value = interest;
        }
        SaveData();
        ApplyLoadedValues();
    }
    public void ApplyLoadedValues()
    {
        int level = CharaterDataManager.Instance.charatorLevel;

        if (levelExpMap.TryGetValue(level, out float maxExp))
        {
            exp_Gauge.maxValue = maxExp;
        }
        else
        {
            exp_Gauge.maxValue = 3600f; // 기본값
        }

        exp_Gauge.value = exp;
        Interest_Gauge.value = interest;

        Lv.text = level.ToString();
        In.text = interest.ToString();
    }

    public void SaveData()
    {
        int expPoint = (int)exp_Gauge.value;
        int interestPoint = (int)Interest_Gauge.value;

        try
        {
            PointSaveData saveData = new PointSaveData
            {
                exp = expPoint,
                interest = interestPoint,
                lastExitTime = DateTime.Now.ToString("o")
            };
            string json = JsonConvert.SerializeObject(saveData, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
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
                PointSaveData saveData = JsonConvert.DeserializeObject<PointSaveData>(json);

                exp = saveData?.exp ?? 0;
                interest = saveData?.interest ?? 0;
                DateTime dlastExitTime = DateTime.Parse(saveData.lastExitTime);
                timeElapsed = DateTime.Now - dlastExitTime;
                lastExitTime = saveData.lastExitTime;
            }
            catch (Exception e)
            {
                Debug.LogError("데이터 로드 오류: " + e.Message);
                exp = 0;
                interest = 0;
            }
        }
        else
        {
            exp = 0;
            interest = 0;
            Debug.Log("저장 파일이 없어 초기화됨.");
        }
        ApplyLoadedValues();
    }

    public void DeleteSaveFile()
    {
        exp = 0;
        interest = 0;
        SaveData(); // 초기값으로 덮어쓰기
        ApplyLoadedValues();
        Debug.Log("캐릭터 데이터가 초기화되었습니다.");
    }

    void OnApplicationQuit()
    {
        SaveData();
    }

    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            SaveData();
        }
    }
}
