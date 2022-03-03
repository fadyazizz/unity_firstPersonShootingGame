using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrimaryBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Text levelNum;

    public void SetMaxAmmo(int maxAmmo)
    {
        slider.maxValue = maxAmmo;
        SetAmmo(0, maxAmmo);
    }
    public void SetAmmo(int currentAmmo, int maxAmmo)
    {
        slider.value = currentAmmo;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        levelNum.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
    }
}
