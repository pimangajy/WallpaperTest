using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeSet : MonoBehaviour
{
    public int charater_level = 0;
    public CharaterType charater_Type = 0;
    public Button btn;


    public void BtnInFun()
    {
        btn.onClick.AddListener(CharatorTyprSet);
    }

    public void CharatorTyprSet()
    {
        PlayerPrefs.SetInt("level", charater_level);
        PlayerPrefs.SetInt("Character_type", (int)charater_Type);
        PlayerPrefs.Save();
    }
}
