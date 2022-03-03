using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAbilityBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Text levelNum;

    public void SetMaxSpecial(int maxSpecial)
    {
        slider.maxValue = maxSpecial;
    }
    public void SetSpecial(int special)
    {
        slider.value = special;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        levelNum.text = special.ToString();
    }
}
