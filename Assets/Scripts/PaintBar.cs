using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxPaint(int paint)
    {
        slider.maxValue = paint;
        slider.value = paint;
    }
    
    public void SetPaint(int paint)
    {
        slider.value = paint; 
    }
}
