using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    public void setExpBar(float exp)
    {
        slider.value = exp;
    }
    public void setMaxExp(float exp)
    {
        slider.maxValue = exp;
    }
}
