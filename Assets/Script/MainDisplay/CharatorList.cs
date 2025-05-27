using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharatorList : MonoBehaviour
{
    public List<CharatorData> charatorDatas; // 인스펙터에 ScriptableObject들 등록


    public CharatorData GetCharatorData(int level, CharaterType type)
    {
        return charatorDatas.Find(c => c.level == level && c.type == type);
    }
}
