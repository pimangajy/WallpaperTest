using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemManager : MonoBehaviour
{
    public Item[] items;             // ������ ���
    public GameObject popupPanel;    // �˾� â
    public Text itemName;            // ������ �̸�
    public Text popupText;           // �˾� â�� �ؽ�Ʈ
    public Button useButton;         // '��' ��ư
    public Button backButton;        // '�ƴϿ�' ��ư
    public Text[] itemCountTexts;    // UI�� ǥ�õǴ� ������ ���� �ؽ�Ʈ

    private int selectedItemIndex;   // ���� ���õ� �������� �ε���

    void Start()
    {

        DOTween.Init();
        // transform �� scale ���� ��� 0.1f�� �����մϴ�.
        transform.localScale = Vector3.one * 0.1f;

        popupPanel.SetActive(false); // �˾� â ��Ȱ��ȭ
        UpdateUI();                  // UI �ʱ�ȭ
    }

    public void SelectItem(int index)
    {
        selectedItemIndex = index;

        // ������ ���� Ȯ��
        if (items[selectedItemIndex].itemCount > 0)
        {
            popupPanel.SetActive(true);
            popupText.text = $"{items[selectedItemIndex].itemName}��(��) ����Ͻðڽ��ϱ�?";
        }
        else
        {
            Debug.Log($"{items[selectedItemIndex].itemName}��(��) �����ϴ�.");
        }
    }

    public void UseItem()
    {
        if (items[selectedItemIndex].itemCount > 0)
        {
            items[selectedItemIndex].itemCount--; // ������ ���� ����

            // ������ ��� ����
            ExecuteItemFunction(items[selectedItemIndex].itemName);

            // ������ ���� ������Ʈ
            UpdateUI();

            // �˾� â �ݱ�
            popupPanel.SetActive(false);
        }
    }

    public void CancelUse()
    {
        popupPanel.SetActive(false);
    }

    private void UpdateUI()
    {
        for (int i = 0; i < items.Length; i++)
        {
            itemCountTexts[i].text = items[i].itemCount.ToString();
        }
    }

    private void ExecuteItemFunction(string itemName)
    {
        switch (itemName)
        {
            case "Health Potion":
                Debug.Log("ü���� ȸ���߽��ϴ�!");
                // ü�� ȸ�� ���� �߰�
                break;

            case "Mana Potion":
                Debug.Log("������ ȸ���߽��ϴ�!");
                // ���� ȸ�� ���� �߰�
                break;

            default:
                Debug.Log("�� �� ���� �������Դϴ�.");
                break;
        }
    }
}
