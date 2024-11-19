using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Terms : MonoBehaviour
{
    [SerializeField] private GameObject fitas_terms;

    public Toggle myToggle;
    public Button myButton;
    private ConfigManager config;
    public float countdownTime;
    private float currentTime;
    public TextMeshProUGUI dinamicText;
    public CalendarioPromocoes calendar;



    [SerializeField] private ScreenChangeEvent screenChangeEvent;

    private void Awake()
    {
        config = new();
        countdownTime = float.Parse(config.GetValue("Timer", "terms"));
    }


    void OnEnable()
    {
        currentTime = countdownTime;
        dinamicText.text = calendar.textoVariavelStep2;
        myButton.onClick.AddListener(OnButtonClicked);
        StartCoroutine(CountdownCoroutine());
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
        ReturnCTA();
    }

    private void ReturnCTA()
    {
        SaveLog();
        screenChangeEvent.OnScreenChange(ScreenType.CTA);
    }

    void OnButtonClicked()
    {
        fitas_terms.gameObject.SetActive(true);
    }

    void SaveLog()
    {
        DataLog dataLog = LogUtil.GetDatalogFromJson();
        dataLog.status = StatusEnum.TERMOS_NAO_ACEITO.ToString();
        LogUtil.SaveLog(dataLog);
    }

}
