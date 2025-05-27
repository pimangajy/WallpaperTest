using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Anime : MonoBehaviour
{
    public Animator animator;

    private void OnEnable()
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

           HairIdle();
            
            yield return new WaitForSeconds(waitTime);  // �ڽ��� �ٽ� ����
        }
    }
    IEnumerator Idle_Arm()
    {
        while (true)  // ���� �ݺ�
        {
            float waitTime = Random.Range(4.0f, 6.0f);  // ���� �ð� ����

            ArmIdle();

            yield return new WaitForSeconds(waitTime);  // �ڽ��� �ٽ� ����
        }
    }
    IEnumerator Idle_Leg()
    {
        while (true)  // ���� �ݺ�
        {
            float waitTime = Random.Range(8.0f, 10.0f);  // ���� �ð� ����

            LegIdle();

            yield return new WaitForSeconds(waitTime);  // �ڽ��� �ٽ� ����
        }
    }

    public void HeadIdle_On()
    {
        animator.SetBool("Head", true);
    }
    public void HeadIdle_Off()
    {
        animator.SetBool("Head", false);
    }

    public void HairIdle()
    {
        animator.SetTrigger("Hair");
    }

    public void ArmIdle()
    {
        animator.SetTrigger("Arm");
    }

    public void LegIdle()
    {
        animator.SetTrigger("Leg");
    }

}
