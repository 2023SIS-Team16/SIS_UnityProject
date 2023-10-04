using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDP_Send : MonoBehaviour
{
    private Thread sendThread;
    private UdpClient client;
    public int port = 5052;
    public bool startSending = true;
    [SerializeField] private GetWebcam _getWebcam;

    [SerializeField] private byte[] _img;
    private bool _send = false;
    private double _counter = 0;
    [SerializeField] private double delay = 1;
    void Start()
    {
        _send = false;
        if (!_getWebcam)
        {
            _getWebcam = GetComponent<GetWebcam>();
        }
        sendThread = new Thread(new ThreadStart(SendData));
        sendThread.IsBackground = true;
        sendThread.Start();
    }

    private void Update()
    {
        _counter = _counter + Time.deltaTime;
        if (_counter > delay)
        {
            Texture2D tex2D = _getWebcam.GetCurrentFrame();
            tex2D = Resize(tex2D, tex2D.height/2, tex2D.height/2);
            _img = tex2D.EncodeToJPG(40);
            _send = true;
            _counter = 0;
        }
    }
    Texture2D Resize(Texture2D texture2D,int targetX,int targetY)
    {
        RenderTexture rt=new RenderTexture(targetX, targetY,24);
        RenderTexture.active = rt;
        Graphics.Blit(texture2D,rt);
        Texture2D result=new Texture2D(targetX,targetY);
        result.ReadPixels(new Rect(0,0,targetX,targetY),0,0);
        result.Apply();
        return result;
    }

    private void OnApplicationQuit()
    {
            try
            {
                client.Close();
                startSending = false;
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
            }
    }

    private void SendData()
    {
        client = new UdpClient(port);
        client.Connect("127.0.0.1", port);
        while (startSending)
        {
            if (_send != true) continue;
            if(_counter < (delay/2)) continue;
            client.Send(_img,_img.Length);
            _send = false;
            Debug.Log(_img.Length);
        }
        client.Close();
    }
}
