using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class EmotionClient : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    private bool isReceiving = false;

    void Start()
    {
        ConnectToPython();
    }

    void ConnectToPython()
    {
        try
        {
            client = new TcpClient("127.0.0.1", 65432);
            stream = client.GetStream();
            Debug.Log("Connected to Python server.");
        }
        catch (SocketException e)
        {
            Debug.Log("SocketException: " + e.ToString());
        }
    }

    public void StartEmotionDetection()
    {
        if (client != null && stream != null)
        {
            byte[] data = Encoding.UTF8.GetBytes("start");
            stream.Write(data, 0, data.Length);
            Debug.Log("Sent 'start' to Python.");

            if (!isReceiving)
            {
                isReceiving = true;
                Thread receiveThread = new Thread(ReceiveEmotion);
                receiveThread.Start();
            }
        }
    }

    void ReceiveEmotion()
    {
        byte[] data = new byte[1024];
        while (true)
        {
            if (stream.DataAvailable)
            {
                int bytes = stream.Read(data, 0, data.Length);
                string receivedEmotion = Encoding.UTF8.GetString(data, 0, bytes);
                Debug.Log("Received emotion from Python: " + receivedEmotion);
                isReceiving = false;
                break;
            }
        }
    }

    void OnApplicationQuit()
    {
        if (client != null)
        {
            stream.Close();
            client.Close();
        }
    }
}
