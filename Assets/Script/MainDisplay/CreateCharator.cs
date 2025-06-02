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
            Debug.Log($"캐릭터 생성 완료: {charData.CharatorName} 레벨 : {charatorLevel} 타입 : {charaterType}");

            if (click != null && spawnchar.GetComponent<Idle_Anime>())
            {
                click.CharaotrIn(spawnchar.GetComponent<Idle_Anime>());
            }
            else Debug.Log("Click 스크립트 혹은 Idle_Anime가 없습니다");
        }
        else
        {
            Debug.LogWarning("해당 조건에 맞는 캐릭터 데이터를 찾을 수 없습니다.");
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
}
