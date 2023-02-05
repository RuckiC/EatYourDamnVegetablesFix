using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinnerBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public float foodGoesColdTime;
    int interval = 1;
    float nextTime = 0;

    private void Start()
    {
        SetMaxTemp(foodGoesColdTime);
    }

    private void Update()
    {
        if (Time.time >= nextTime)
        {

            foodGoesColdTime -= 1;
            SetTemp(foodGoesColdTime);

            nextTime += interval;

            if (foodGoesColdTime <= 0)
            {
                Debug.Log("Food Gone Cold, SUCK IT MOM!");
            }
        }
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
}
