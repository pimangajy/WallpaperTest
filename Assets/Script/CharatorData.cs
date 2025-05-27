using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharaterType
{
    Default,
    yuniNomal
}

[CreateAssetMenu(fileName = "CharatorData", menuName = "Scriptable Objects/Charator Data")]
public class CharatorData : ScriptableObject
{
    public GameObject Charator;
    public string CharatorName;
    public string age;
    public string birthday;
    public string mbti;
    [TextArea(5, 10)]
    public string explanation;
    public Sprite main_Image;
    public Sprite hide_Sprite;

    public int level;

    public CharaterType type;
}
