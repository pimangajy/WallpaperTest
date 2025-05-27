using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAssetManager : MonoBehaviour
{
    public static CharacterAssetManager Instance { get; private set; }

    // 레벨별로 캐릭터 에셋 리스트를 설정할 수 있는 구조체
    [System.Serializable]
    public struct LevelCharacterMapping
    {
        public int level;                          // 레벨 값 (예: 1, 2, 3 등)
        public List<CharatorData> charatorDataList;  // 해당 레벨에서 가능한 캐릭터 에셋 리스트
    }

    // 에디터에서 할당할 수 있게 public으로 노출합니다.
    [SerializeField]
    private List<LevelCharacterMapping> levelCharacterMappings;

    // 2중 Dictionary: key1은 레벨, key2는 타입, value는 해당 CharatorData 에셋
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

    // 에디터에서 할당된 데이터를 기반으로 2중 Dictionary 생성
    private void BuildMapping()
    {
        assetMapping = new Dictionary<int, Dictionary<CharaterType, CharatorData>>();
        foreach (var mapping in levelCharacterMappings)
        {
            // 만약 해당 레벨이 이미 등록되어 있다면 추가하지 않고 경고 로깅
            if (assetMapping.ContainsKey(mapping.level))
            {
                Debug.LogWarning($"레벨 {mapping.level}에 대한 매핑이 이미 존재합니다.");
                continue;
            }

            // 레벨에 해당하는 Dictionary 생성
            Dictionary<CharaterType, CharatorData> typeDict =
                new Dictionary<CharaterType, CharatorData>();

            // 해당 레벨에서 사용 가능한 모든 캐릭터 에셋을 타입별로 추가
            foreach (var data in mapping.charatorDataList)
            {
                if (!typeDict.ContainsKey(data.type))
                {
                    typeDict.Add(data.type, data);
                }
                else
                {
                    Debug.LogWarning($"레벨 {mapping.level}에 이미 {data.type} 타입의 데이터가 등록되어 있습니다.");
                }
            }
            assetMapping.Add(mapping.level, typeDict);
        }
    }

    // 제공된 레벨과 타입에 해당하는 CharatorData 에셋을 반환하는 함수
    public CharatorData GetCharatorData(int level, CharaterType type)
    {
        if (assetMapping.TryGetValue(level, out var typeDict))
        {
            if (typeDict.TryGetValue(type, out var data))
            {
                return data;
            }
            Debug.LogWarning($"레벨 {level}에서 타입 {type}에 해당하는 데이터를 찾을 수 없습니다.");
            return null;
        }
        Debug.LogWarning($"레벨 {level}에 대한 매핑이 존재하지 않습니다.");
        return null;
    }
}
