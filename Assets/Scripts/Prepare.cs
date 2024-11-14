using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prepare : MonoBehaviour
{
    [SerializeField] private ScreenChangeEvent screenChangeEvent;
    public RawImage webcamTexture;
    public float countdownTime;
    private float currentTime;
    public Text textCount;

    private ConfigManager config;

    private void Awake()
    {
        config = new();

        countdownTime = float.Parse(config.GetValue("Timer", "prepare"));
    }

    private void OnEnable()
    {
        textCount.gameObject.SetActive(true);
        webcamTexture.GetComponent<RawImage>().enabled = true;
        currentTime = countdownTime;
        StartCoroutine(CountdownCoroutine());
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeScreen();
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        while (currentTime > 0)
        {
            textCount.text = currentTime.ToString("F0");
            currentTime -= Time.deltaTime;
            yield return null;
        }

        OnCountdownFinished();
    }

    private void OnCountdownFinished()
    {
        ChangeScreen();
    }

    private void ChangeScreen()
    {
        screenChangeEvent.OnScreenChange(ScreenType.SCANNING);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        textCount.gameObject.SetActive(false);
    }


}

