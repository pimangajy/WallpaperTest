using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class Encyclopedia_Button : MonoBehaviour
{
    public UI_Event UIEvent;
    public Image icon;
    public Text charaterName;
    public string charatorName;

    public Encyclopedia_Panel panel;
    [SerializeField]
    private CharatorData charaterData;

    public PanelHandler panelHandler;
    public Button btn;

    private void Awake()
    {
        if (UIEvent != null)
        {
            UIEvent.itemSlotOpen += EncyclopediaSlot;
        }
    }

    //  목록에서 보일 이미지 처리
    private void EncyclopediaSlot(object sender, EventArgs e)
    {
        bool discovered = CharaterDataManager.Instance.IsCharaterDiscovered(charaterData);
        icon.sprite = discovered ? charaterData.main_Image : charaterData.hide_Sprite;
        charaterName.text = discovered ? charaterData.name : charatorName;
        Debug.Log("도감 이미지");
    }

    public void Open()
    {
        bool discovered = CharaterDataManager.Instance.IsCharaterDiscovered(charaterData);

        panel.character_name.text = discovered ? charaterData.CharatorName : "???";
        panel.age.text = discovered ? charaterData.age : "???";
        panel.birthday.text = discovered ? charaterData.birthday : "???";
        panel.mbti.text = discovered ? charaterData.mbti : "???";
        panel.explanation.text = discovered ? charaterData.explanation : "???";
        // panel.count.text = count.ToString();
        panel.main_image.sprite = discovered ? charaterData.main_Image : charaterData.hide_Sprite;
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(CharatorTyprSet);
        btn.onClick.AddListener(panelHandler.Hide);
    }

    public void CharatorTyprSet()
    {
        PlayerPrefs.SetInt("level", charaterData.level);
        PlayerPrefs.SetInt("Character_type", (int)charaterData.type);
        PlayerPrefs.Save();

        Debug.Log("캐릭터 변경 완료");
    }
}
