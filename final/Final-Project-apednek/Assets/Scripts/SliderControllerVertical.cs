using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControllerVertical : MonoBehaviour
{
    Camera cam;
    [SerializeField] Slider slider;
    float value;

    void Start()
    {
        cam = Camera.main;
        slider.onValueChanged.AddListener(this.OnSliderChanged);
        value = slider.value;
    }

    
    void OnSliderChanged(float currentValue)
    {
        float offset = currentValue - value;
        cam.transform.Rotate(Vector3.right * offset * 50);
        value = currentValue;
    }
}
