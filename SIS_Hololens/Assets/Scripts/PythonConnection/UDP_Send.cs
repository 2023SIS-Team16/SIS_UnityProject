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
    private bool send = false;
    private double counter = 0;
    [SerializeField] private double delay = 1;
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
        counter = counter + Time.deltaTime;
        if (counter > delay)
        {
            _img =_getWebcam.GetCurrentFrame().EncodeToPNG();
            send = true;
            counter = 0;
        }
        

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
            if (send != true) continue;
            if(counter < (delay/2)) continue;
            client.Send(_img,port);
            send = false;
            Debug.Log(_img.Length);
        }
        client.Close();
    }
}
