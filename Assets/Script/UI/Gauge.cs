using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public Slider esp_slider;
    public Slider interest_slider;
    public bool exp;

    public void MaxGauge(int max_Gauge)
    {
        esp_slider.maxValue = max_Gauge;
    }

    public void SetGauge(int val)
    {
        esp_slider.value = val;
    }

    public void PlusGauge(int val)
    {
        esp_slider.value += val;
    }

    private void Start()
    {
        if(PlayerPrefs.HasKey("exp"))
        {
            esp_slider.value = PlayerPrefs.GetFloat("exp");
            interest_slider.value = PlayerPrefs.GetFloat("interest");
        }else
        {
            PlayerPrefs.SetFloat("exp", esp_slider.value);
            PlayerPrefs.SetFloat("interest", interest_slider.value);
        }

        PlayerPrefs.Save();
    }

    private void Update()
    {
        if (exp)
        {
            esp_slider.value += Time.deltaTime * 1.0f;
            esp_slider.value = Mathf.Clamp(esp_slider.value, esp_slider.minValue, esp_slider.maxValue);
        }

    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            
            PlayerPrefs.SetFloat("exp", esp_slider.value);
            
            PlayerPrefs.SetFloat("interest", interest_slider.value);
            
            PlayerPrefs.Save();
        }
    }
}
