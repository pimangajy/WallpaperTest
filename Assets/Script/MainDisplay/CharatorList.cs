using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharatorList : MonoBehaviour
{
    public List<CharatorData> charatorDatas; // �ν����Ϳ� ScriptableObject�� ���


    public CharatorData GetCharatorData(int level, CharaterType type)
    {
        return charatorDatas.Find(c => c.level == level && c.type == type);
    }
}
