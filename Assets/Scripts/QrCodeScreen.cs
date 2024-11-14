using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QrCodeScreen : MonoBehaviour
{
    [SerializeField] private ScreenChangeEvent screenChangeEvent;
    private ConfigManager config;

    public Text text;

    public float time;

    private void Awake()
    {
        config = new();
    }

    private void OnEnable()
    {
        GetCupom();
        Invoke(nameof(GoToCTA), time);
    }

    void GetCupom()
    {
        string emotionSaved = PlayerPrefs.GetString("emotion");
        string cupom = config.GetValue("Emotion", emotionSaved);
        text.text = cupom;
    }

    void GoToCTA()
    {
        screenChangeEvent.OnScreenChange(ScreenType.CTA);
        PlayerPrefs.DeleteAll();
        gameObject.SetActive(false);
    }
}
