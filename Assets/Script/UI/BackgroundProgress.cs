using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEditor.Overlays;

public class BackgroundProgress : MonoBehaviour
{
    private static BackgroundProgress _instance;
    public static BackgroundProgress Instance { get; private set; }
    private string filePath;

    public Slider exp_Gauge;
    public Slider Interest_Gauge;
    public Text Lv;
    public Text In;

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
            DontDestroyOnLoad(gameObject);
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

            // 경험치 증가
            float gainedExp = elapsedSeconds * 1.0f;
            exp_Gauge.value = Mathf.Clamp(exp_Gauge.value + gainedExp, exp_Gauge.minValue, exp_Gauge.maxValue);
            exp = (int)exp_Gauge.value;
            Lv.text = exp.ToString();

            // 애정도 감소
            float lostInterest = elapsedSeconds * (10f / 3600f);  // 시간당 10 감소
            Interest_Gauge.value = Mathf.Clamp(Interest_Gauge.value - lostInterest, Interest_Gauge.minValue, Interest_Gauge.maxValue);
            interest = (int)Interest_Gauge.value;
            In.text = interest.ToString();

            SaveData();  // 갱신된 값 저장
        }
    }
    public void SaveData()
    {
        try
        {
            PointSaveData saveData = new PointSaveData
            {
                exp = this.exp,
                interest = this.interest,
                lastExitTime = DateTime.Now.ToString("o")
            };
            string json = JsonConvert.SerializeObject(saveData, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
            Debug.Log(saveData.exp);
            Debug.Log(saveData.interest);
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
    }

    public void DeleteSaveFile()
    {
        exp = 0;
        interest = 0;

        SaveData(); // 초기값으로 덮어쓰기
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
