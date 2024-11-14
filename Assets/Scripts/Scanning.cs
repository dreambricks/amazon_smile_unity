using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scanning : MonoBehaviour
{
    [SerializeField] private ScreenChangeEvent screenChangeEvent;
    [SerializeField] private UDPReceiver udpReceiver;

    public RawImage webcamTexture;

    private void OnEnable()
    {
        Debug.Log("Prepare to send message");
        udpReceiver.SendMessage("start");
    }

    private void Update()
    {
        ChangeScreen();
        UdpRead();
    }

    private void UdpRead()
    {
        string data = udpReceiver.GetLastestData();

        if (data == "rindo")
        {
            screenChangeEvent.OnScreenChange(ScreenType.ANALISING);
            PlayerPrefs.SetString("emotion", "rindo");
            gameObject.SetActive(false);
        }

        else if (data == "neutro")
        {
            screenChangeEvent.OnScreenChange(ScreenType.ANALISING);
            PlayerPrefs.SetString("emotion", "neutro");
            gameObject.SetActive(false);
        }

        else if (data == "sorrindo")
        {
            screenChangeEvent.OnScreenChange(ScreenType.ANALISING);
            PlayerPrefs.SetString("emotion", "sorrindo");
            gameObject.SetActive(false);
        }
    }

    private void ChangeScreen()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            screenChangeEvent.OnScreenChange(ScreenType.ANALISING);
            gameObject.SetActive(false);
        }
    }


    private void OnDisable()
    {
        webcamTexture.GetComponent<RawImage>().enabled = false;
    }
}
