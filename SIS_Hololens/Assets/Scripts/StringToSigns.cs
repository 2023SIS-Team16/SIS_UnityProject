using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StringToSigns : MonoBehaviour
{
    private Dictionary<char, SignScriptableObject> _charDictionary = new Dictionary<char, SignScriptableObject>();
    [SerializeField] private SignScriptableObject[] _charScriptableObjects;
    //public String testString = "Test";
    private Queue _characterQueue = new Queue();

    private float _timer = 0;
    [SerializeField] private float _timeBuffer = 2;
    
    [SerializeField] private Image imageToReplace;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Slider slider;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        InitialiseHashTable();
    }

    // Update is called once per frame
    void Update()
    {
        if (slider)
        {
            _timeBuffer = (0.5f + ((float)slider.value) * 3);
        }
        
        if (Input.GetKeyUp("q"))
        {
            SetQueue();
        }

        if (_characterQueue.Count > 0 && _timer <= 0)
        {
            ProcessQueue();
            _timer = _timeBuffer;
        }

        if (_timer > 0)
        {
            _timer = _timer - Time.deltaTime;
        }
    }
    private void ProcessString(String str)
    {
        if(str.Length == 0){ return; }
        
        foreach (var character in str)
        {
            if (_charDictionary.ContainsKey(character))
            {
                char tempChar = _charDictionary[character].getCharacter();
                _characterQueue.Enqueue(tempChar);
            }

        }
    }

    private void ProcessQueue()
    {
        char tempChar = (char)_characterQueue.Dequeue();
        imageToReplace.sprite = _charDictionary[tempChar].signSprite;
        Debug.Log(tempChar + " set");
    }

    private void InitialiseHashTable()
    {
        foreach (var charSO in _charScriptableObjects)
        {
            _charDictionary.Add(charSO.getCharacter(), charSO);
        }
    }

    public float GetTimeBuffer()
    {
        return _timeBuffer;
    }

    public void SetQueue()
    {
        _characterQueue.Clear();
        String tempString = inputField.text.ToLower();
        ProcessString(tempString);
        _timer = 0;
    }
}
