using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonoBehaviour
{
    [SerializeField]
    private List<Idle_Anime> idle_Animes = new List<Idle_Anime>();

    void Start()
    {
        StartCoroutine(Idle_Hair());  // �ڷ�ƾ ����
        StartCoroutine(Idle_Arm());
        StartCoroutine(Idle_Leg());
    }


    IEnumerator Idle_Hair()
    {
        while (true)  // ���� �ݺ�
        {
            float waitTime = Random.Range(5.0f, 8.0f);  // ���� �ð� ����

            foreach (Idle_Anime anime in idle_Animes)
            {
                anime.HairIdle();
            }

            yield return new WaitForSeconds(waitTime);  // �ڽ��� �ٽ� ����
        }
    }
    IEnumerator Idle_Arm()
    {
        while (true)  // ���� �ݺ�
        {
            float waitTime = Random.Range(4.0f, 6.0f);  // ���� �ð� ����

            foreach (Idle_Anime anime in idle_Animes)
            {           
                anime.ArmIdle();

            }

            yield return new WaitForSeconds(waitTime);  // �ڽ��� �ٽ� ����
        }
    }
    IEnumerator Idle_Leg()
    {
        while (true)  // ���� �ݺ�
        {
            float waitTime = Random.Range(8.0f, 10.0f);  // ���� �ð� ����

            foreach (Idle_Anime anime in idle_Animes)
            {
                anime.LegIdle();
            }

            yield return new WaitForSeconds(waitTime);  // �ڽ��� �ٽ� ����
        }
    }
}
