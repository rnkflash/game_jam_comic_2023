using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarAnimated : MonoBehaviour
{
    private float maxValue = 100;
    public float currentValue = 0;

    private Slider slider;
    
    void Awake() {
        slider = GetComponent<Slider>();
        SetValue(currentValue);
    }

    public void SetProgress(float percent) {
        if (slider != null)
            slider.value = percent;
    }

    public void SetValue(float value) 
    {
        currentValue = value;
        SetProgress(Mathf.Clamp(currentValue, 0, maxValue)/maxValue);
    }

    public void Init(int value, int max) {
        currentValue = value;
        maxValue = max;
        SetProgress(Mathf.Clamp(currentValue, 0, maxValue)/maxValue);
    }

}
