using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharator : MonoBehaviour
{
    public CharatorList CharatorList;
    public Click click;

    void Start()
    {
        CharaterSpawn();
    }

    public void CharaterSpawn()
    {
        int savedLevel = PlayerPrefs.GetInt("level");
        int savedType = PlayerPrefs.GetInt("Character_type");

        CharaterType type = (CharaterType)savedType;

        CharatorData charData = CharatorList.GetCharatorData(savedLevel, type);

        if (charData != null)
        {
            GameObject spawnchar = Instantiate(charData.Charator, transform.position, Quaternion.identity);
            Debug.Log($"캐릭터 생성 완료: {charData.CharatorName}");

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
}
