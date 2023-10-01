using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.WebCam;

public class GetWebcam : MonoBehaviour
{
    private WebCamTexture _webCamTexture;
    [SerializeField] private Texture2D frame;
    private int _captureCount = 0;
    
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
        if (_webCamTexture.didUpdateThisFrame)
        {
            frame = new Texture2D(_webCamTexture.width, _webCamTexture.height);
            frame.SetPixels(_webCamTexture.GetPixels());
            frame.Apply();
            
            // string path = @"F:/temp/UnityTest/";
            //
            // string savePath = path;
            // System.IO.File.WriteAllBytes(savePath + _captureCount.ToString() + ".png", frame.EncodeToPNG());
            // _captureCount++;
        }
    }

    public Texture2D GetCurrentFrame()
    {
        if (frame)
        {
            return frame;
        }

        return new Texture2D(_webCamTexture.width, _webCamTexture.height);
    }
}
