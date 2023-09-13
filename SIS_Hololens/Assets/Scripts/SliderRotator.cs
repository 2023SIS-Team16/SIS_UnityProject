using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderRotator : MonoBehaviour
{
    [SerializeField] private Transform toRotate;
    [SerializeField] private Slider slider;
    private Vector3 eulerAngles = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!toRotate) { return; }
        if(!slider) { return; }
        eulerAngles.y = -slider.value * 360;

        toRotate.eulerAngles = eulerAngles;
    }
}
