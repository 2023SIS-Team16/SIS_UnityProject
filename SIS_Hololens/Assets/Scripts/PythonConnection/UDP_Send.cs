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

    private byte[] _img;
    void Start()
    {
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
        _img =_getWebcam.GetCurrentFrame().EncodeToPNG();
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
                if (_img != null)
                {
                    client.Send(_img,port);
                    //Debug.Log(_img.Length);
                }
        }
        client.Close();
    }
}
