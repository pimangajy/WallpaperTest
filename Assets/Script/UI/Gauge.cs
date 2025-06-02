using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gauge : MonoBehaviour
{
    public Slider esp_slider;
    public Slider interest_slider;
    public Text expValue;
    public Text interestValue;
    public bool exp;


    private void Update()
    {
        if (exp)
        {
            esp_slider.value += Time.deltaTime * 1.0f;
            esp_slider.value = Mathf.Clamp(esp_slider.value, esp_slider.minValue, esp_slider.maxValue);
        }else
        {
            interest_slider.value = Mathf.Floor(interest_slider.value);
            interestValue.text = interest_slider.value.ToString("f0");
        }

    }

}
