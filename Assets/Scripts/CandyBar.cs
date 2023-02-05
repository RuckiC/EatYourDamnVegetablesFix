using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandyBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public float sugarHighTime;
    public float interval = 0.25f;
    public float drainRate = 0.25f;
    float nextTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetMaxSugarHigh(sugarHighTime);
    }

    private void Update()
    {
        if (Time.time >= nextTime)
        {

            sugarHighTime -= drainRate;
            SetSugarHigh(sugarHighTime);

            nextTime += interval;

            if (sugarHighTime <= 0)
            {
                Debug.Log("No more sugar to the brain, bodily function have shut down");
            }
        }

        if(sugarHighTime > 240)
        {
            sugarHighTime = 240;
        }
    }

    public void SetMaxSugarHigh(float sugarLevel)
    {
        slider.maxValue = sugarLevel;
        slider.value = sugarLevel;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetSugarHigh(float sugarLevel)
    {
        slider.value = sugarLevel;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
