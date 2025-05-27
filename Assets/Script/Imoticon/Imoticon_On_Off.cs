using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imoticon_On_Off : MonoBehaviour
{
    
    public void Imoticon_On()
    {
        gameObject.SetActive(true);
    }

    public void Imoticon_Off()
    {
        gameObject.SetActive(false);
    }

    public void Surprise_On_Off(float a)
    {
        gameObject.SetActive(true);

        Invoke("Imoticon_Off", a);
    }
}
