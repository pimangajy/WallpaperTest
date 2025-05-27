using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Level_Button;

public class GetCharator : MonoBehaviour
{
    CreateCharator CreateCharator;
    CharatorData CharatorData;

    public Text type;
    public Text level;

    private void Awake()
    {
        GetLevelType();
    }

    public void GetLevelType()
    {
        if (!PlayerPrefs.HasKey("Character_type") && !PlayerPrefs.HasKey("level"))
        {
            Debug.Log("저장되어있는 레벨과 탕비이 없음");
            PlayerPrefs.SetInt("Character_type", 0);
            PlayerPrefs.SetInt("level", 0);
        }
        else
        {
            type.text = "Type: " + PlayerPrefs.GetInt("Character_type").ToString();
            level.text = "Level: " + PlayerPrefs.GetInt("level").ToString();
        }

        
    }
}
