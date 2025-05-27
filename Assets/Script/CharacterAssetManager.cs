using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAssetManager : MonoBehaviour
{
    public static CharacterAssetManager Instance { get; private set; }

    // �������� ĳ���� ���� ����Ʈ�� ������ �� �ִ� ����ü
    [System.Serializable]
    public struct LevelCharacterMapping
    {
        public int level;                          // ���� �� (��: 1, 2, 3 ��)
        public List<CharatorData> charatorDataList;  // �ش� �������� ������ ĳ���� ���� ����Ʈ
    }

    // �����Ϳ��� �Ҵ��� �� �ְ� public���� �����մϴ�.
    [SerializeField]
    private List<LevelCharacterMapping> levelCharacterMappings;

    // 2�� Dictionary: key1�� ����, key2�� Ÿ��, value�� �ش� CharatorData ����
    private Dictionary<int, Dictionary<CharaterType, CharatorData>> assetMapping;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            BuildMapping();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �����Ϳ��� �Ҵ�� �����͸� ������� 2�� Dictionary ����
    private void BuildMapping()
    {
        assetMapping = new Dictionary<int, Dictionary<CharaterType, CharatorData>>();
        foreach (var mapping in levelCharacterMappings)
        {
            // ���� �ش� ������ �̹� ��ϵǾ� �ִٸ� �߰����� �ʰ� ��� �α�
            if (assetMapping.ContainsKey(mapping.level))
            {
                Debug.LogWarning($"���� {mapping.level}�� ���� ������ �̹� �����մϴ�.");
                continue;
            }

            // ������ �ش��ϴ� Dictionary ����
            Dictionary<CharaterType, CharatorData> typeDict =
                new Dictionary<CharaterType, CharatorData>();

            // �ش� �������� ��� ������ ��� ĳ���� ������ Ÿ�Ժ��� �߰�
            foreach (var data in mapping.charatorDataList)
            {
                if (!typeDict.ContainsKey(data.type))
                {
                    typeDict.Add(data.type, data);
                }
                else
                {
                    Debug.LogWarning($"���� {mapping.level}�� �̹� {data.type} Ÿ���� �����Ͱ� ��ϵǾ� �ֽ��ϴ�.");
                }
            }
            assetMapping.Add(mapping.level, typeDict);
        }
    }

    // ������ ������ Ÿ�Կ� �ش��ϴ� CharatorData ������ ��ȯ�ϴ� �Լ�
    public CharatorData GetCharatorData(int level, CharaterType type)
    {
        if (assetMapping.TryGetValue(level, out var typeDict))
        {
            if (typeDict.TryGetValue(type, out var data))
            {
                return data;
            }
            Debug.LogWarning($"���� {level}���� Ÿ�� {type}�� �ش��ϴ� �����͸� ã�� �� �����ϴ�.");
            return null;
        }
        Debug.LogWarning($"���� {level}�� ���� ������ �������� �ʽ��ϴ�.");
        return null;
    }
}
