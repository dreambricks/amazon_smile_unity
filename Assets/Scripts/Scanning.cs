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
        udpReceiver.SendMessage("start");
    }

    private void Update()
    {
        ChangeScreen();
        UdpRead();
    }

    private void UdpRead()
    {
        if (udpReceiver.GetLastestData() == "sorrindo")
        {
            screenChangeEvent.OnScreenChange(ScreenType.ANALISING);
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
