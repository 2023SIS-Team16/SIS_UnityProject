using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreen : MonoBehaviour
{
    [SerializeField] private bool fullscreen;

    private void Start()
    {
        fullscreen = Screen.fullScreen;
        QualitySettings.vSyncCount = 0;
    }

    public void SetFullscreen()
    {
        fullscreen = !fullscreen;
        Screen.fullScreen = fullscreen;
    }
}
