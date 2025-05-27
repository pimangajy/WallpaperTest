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
            Debug.Log($"ĳ���� ���� �Ϸ�: {charData.CharatorName}");

            if (click != null && spawnchar.GetComponent<Idle_Anime>())
            {
                click.CharaotrIn(spawnchar.GetComponent<Idle_Anime>());
            }
            else Debug.Log("Click ��ũ��Ʈ Ȥ�� Idle_Anime�� �����ϴ�");
        }
        else
        {
            Debug.LogWarning("�ش� ���ǿ� �´� ĳ���� �����͸� ã�� �� �����ϴ�.");
        }
    }
}
