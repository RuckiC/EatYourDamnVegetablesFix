using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinnerBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public float currentTemp;
    public float maxTemp;

    private void Start()
    {
        currentTemp = maxTemp;
        SetMaxTemp(maxTemp);
    }

    private void Update()
    {
        StartCoroutine(DinnerTemp());
    }


    public void SetMaxTemp(float temp)
    {
        slider.maxValue = temp;
        slider.value = temp;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetTemp(float temp)
    {
        slider.value = temp;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    IEnumerator DinnerTemp()
    {
        while (currentTemp > 0)
        {
            currentTemp -= 1;
            SetTemp(currentTemp);
            yield return new WaitForSeconds(5f);
        }
    }
}
