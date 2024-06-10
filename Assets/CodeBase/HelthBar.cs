using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;



    public void SetMaxHealth(int maxHelth)
    {
        _slider.maxValue = maxHelth;
        _slider.value = maxHelth;
    }
    public void Sethealth(int health)
    {
        _slider.value = health;
    }
}
