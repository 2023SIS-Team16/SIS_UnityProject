using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.WebCam;

public class GetWebcam : MonoBehaviour
{
    private WebCamTexture _webCamTexture;
    
    // Start is called before the first frame update
    void Start()
    {
        _webCamTexture = new WebCamTexture();
        GetComponent<RawImage>().material.mainTexture = _webCamTexture;
        _webCamTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
