using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hourglass : MonoBehaviour
{
    float maxFill = 100f;
    float currentFill = 50f;
    public Image topSand;
    public Image bottomSand;
    public Slider hourglassTester;

    public void SetFill(float fillAmount ) // sand falls from top to fill the bottom
    {
        currentFill = fillAmount;

        topSand.fillAmount = 0.5f+ fillAmount/200f;
        bottomSand.fillAmount = 0.5f - fillAmount/200f;
    }
}
