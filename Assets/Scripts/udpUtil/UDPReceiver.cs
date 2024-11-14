using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceiver : MonoBehaviour
{
    Thread receiverThread;
    UdpClient client;
    public int port = 5005;  // Defina a porta desejada
    public int port_sender = 5006;
    public bool startReceiving = true;
    public bool printToConsole = true;
    public string data;
    public static LockFreeQueue<string> myQueue;

    public void Start()
    {
        myQueue = new LockFreeQueue<string>();
        receiverThread = new Thread(new ThreadStart(ReceiveData));
        receiverThread.IsBackground = true;
        receiverThread.Start();
    }

    private void ReceiveData()
    {
        client = new UdpClient(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));

        while (startReceiving)
        {
            try
            {
                IPEndPoint localIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
                byte[] dataByte = client.Receive(ref localIP);
                data = Encoding.UTF8.GetString(dataByte);
                myQueue.Enqueue(data);

                if (printToConsole) { print(data); }
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    public string GetData()
    {
        if (myQueue.Empty()) return "";

        return myQueue.Dequeue();
    }

    public string GetLastestData()
    {
        string result = "";
        string data = "";
        while ((data = GetData()) != "")
        {
            result = data;
        }

        return result;
    }


    public void SendMessage(string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port_sender);

            client.Send(data, data.Length, endPoint);
            Console.WriteLine($"Mensagem enviada: {message}");
            Debug.Log($"Mensagem enviada: {message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro ao enviar mensagem: {e}");
        }
    }
}
