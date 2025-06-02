using System;
using UnityEngine;

public class UI_Event : MonoBehaviour
{
    public event EventHandler itemSlotOpen;
    public event EventHandler encyclopediaSlotOpen;
    public event EventHandler settingSlotIOpen;

    public void Start()
    {
        ItemSlotOpen();
        EncyclopediaSlotOpen();
    }

    // �����۸�� �̹��� ó��
    public void ItemSlotOpen()
    {
        itemSlotOpen?.Invoke(this, EventArgs.Empty);
    }

    // ���� �̹��� ó��
    public void EncyclopediaSlotOpen()
    {
        encyclopediaSlotOpen?.Invoke(this, EventArgs.Empty);
    }

    public void SettingSlotIOpen()
    {
        settingSlotIOpen?.Invoke(this, EventArgs.Empty);
    }

}
