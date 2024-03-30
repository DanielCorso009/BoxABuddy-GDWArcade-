using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SB2 : MonoBehaviour
{
    public Slider slider;

    public void SetMaxStamina(float stam){
        slider.maxValue = stam;
        slider.value = stam;
    }
    public void SetStamina(float stam){
        slider.value = stam;
    }
}
