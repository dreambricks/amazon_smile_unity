using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scanning : MonoBehaviour
{
    [SerializeField] private ScreenChangeEvent screenChangeEvent;
    [SerializeField] private UDPReceiver udpReceiver;
    public float countdownTime;
    private float currentTime;
    public RawImage webcamTexture;
    private ConfigManager config;


    private void Awake()
    {
        config = new();
        countdownTime = float.Parse(config.GetValue("Timer", "scanning"));
    }

    private void OnEnable()
    {
        currentTime = countdownTime;
        Debug.Log("Prepare to send message");
        udpReceiver.SendMessage("start");
        StartCoroutine(CountdownCoroutine());
    }

    private void Update()
    {
        PressToChangeScreen();
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

    private void PressToChangeScreen()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeScreen();
        }
    }


    private void ChangeScreen()
    {
        screenChangeEvent.OnScreenChange(ScreenType.ANALISING);
        PlayerPrefs.SetString("emotion", "sorrindo");
        gameObject.SetActive(false);
    }

    private IEnumerator CountdownCoroutine()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            yield return null;
        }

        OnCountdownFinished();
    }

    private void OnCountdownFinished()
    {
        ChangeScreen();
    }


    private void OnDisable()
    {
        webcamTexture.GetComponent<RawImage>().enabled = false;
    }
}
