using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Linq;

public class ItemDataManager : MonoBehaviour
{
    private static ItemDataManager _instance;
    public static ItemDataManager Instance { get; private set; }

    private Dictionary<string, bool> discoveredItems = new Dictionary<string, bool>();
    private Dictionary<string, int> itemCounts = new Dictionary<string, int>();

    private string filePath;

    [Serializable]
    public class ItemSaveData
    {
        public Dictionary<string, bool> discoveredItems;
        public Dictionary<string, int> itemCounts;
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

        filePath = Application.persistentDataPath + "/itemSaveData.json";
        LoadData();
    }

    public bool IsItemDiscovered(ItemData item)
    {
        return discoveredItems.TryGetValue(item.itemName, out bool isDiscovered) && isDiscovered;
    }

    public void DiscoverItem(ItemData item)
    {
        if (!discoveredItems.ContainsKey(item.itemName))
        {
            discoveredItems[item.itemName] = true;
        }
    }

    public int GetItemCount(ItemData item)
    {
        return itemCounts.TryGetValue(item.itemName, out int count) ? count : 0;
    }

    public void AddItem(ItemData item, int amount)
    {
        if (!itemCounts.ContainsKey(item.itemName))
        {
            itemCounts[item.itemName] = 0;
        }
        DiscoverItem(item);
        itemCounts[item.itemName] += amount;
        Debug.Log(item.itemName + " " + GetItemCount(item));
        SaveData();
    }

    public bool UseItem(ItemData item)
    {
        if (itemCounts.TryGetValue(item.itemName, out int count) && count > 0)
        {
            itemCounts[item.itemName]--;
            SaveData();
            return true;
        }
        else
        {
            Debug.Log($"{item.itemName} 아이템이 부족합니다.");
            return false;
        }
    }
    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        try
        {
            //var saveData = new { discoveredItems, itemCounts };
            ItemSaveData saveData = new ItemSaveData
            {
                discoveredItems = this.discoveredItems,
                itemCounts = this.itemCounts
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
                ItemSaveData saveData = JsonConvert.DeserializeObject<ItemSaveData>(json);

                discoveredItems = saveData?.discoveredItems ?? new Dictionary<string, bool>();
                itemCounts = saveData?.itemCounts ?? new Dictionary<string, int>();
            }
            catch (Exception e)
            {
                Debug.LogError("데이터 로드 오류: " + e.Message);
                discoveredItems = new Dictionary<string, bool>();
                itemCounts = new Dictionary<string, int>();
            }
        }
        else
        {
            discoveredItems = new Dictionary<string, bool>();
            itemCounts = new Dictionary<string, int>();
            Debug.Log("저장 파일이 없어 초기화됨.");
        }
    }
    public void DeleteSaveFile()
    {
        discoveredItems = new Dictionary<string, bool>();
        itemCounts = new Dictionary<string, int>();

        SaveData(); // 초기값으로 덮어쓰기
        Debug.Log("캐릭터 데이터가 초기화되었습니다.");
    }
}
