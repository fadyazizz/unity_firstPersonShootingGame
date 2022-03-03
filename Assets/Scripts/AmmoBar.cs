using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Text levelNum;

    public void SetMaxAmmo(int maxAmmo)
    {
        slider.maxValue = 150;
        SetAmmo(0, 150);
    }
    public void SetAmmo(int currentAmmo, int maxAmmo)
    {
        slider.value = currentAmmo;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        levelNum.text = currentAmmo.ToString() + "/" + "150";
    }
}
