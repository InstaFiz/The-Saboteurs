using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script automatically changes the color of the stress bar depending on the amount of stress

public class StressBarColorChange : MonoBehaviour
{
    private Slider stressBar;
    private Image backgroundImage;
    private Image fillImage;

    void Start()
    {
        stressBar = GetComponent<Slider>();
        backgroundImage = transform.GetChild(0).gameObject.GetComponent<Image>();
        fillImage = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (stressBar.value > 0.6f)
        {
            fillImage.color = new Color32(255, 0, 0, 255);
            backgroundImage.color = new Color32(150, 0, 0, 255);
        }

        if (stressBar.value <= 0.6f && stressBar.value > 0.3f)
        {
            fillImage.color = new Color32(255, 255, 0, 255);
            backgroundImage.color = new Color32(150, 150, 0, 255);
        }

        if (stressBar.value < 0.3f)
        {
            fillImage.color = new Color32(0, 255, 0, 255);
            backgroundImage.color = new Color32(0, 150, 0, 255);
        }
    }
}
