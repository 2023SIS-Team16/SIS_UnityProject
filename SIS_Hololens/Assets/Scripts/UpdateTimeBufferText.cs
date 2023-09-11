using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateTimeBufferText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private StringToSigns _stringToSigns; 

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_text){return;}
        if(!_stringToSigns){return;}

        _text.text = _stringToSigns.GetTimeBuffer().ToString();
    }
}
