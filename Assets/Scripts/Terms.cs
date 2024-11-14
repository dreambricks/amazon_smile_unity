using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private ScreenChangeEvent screenChangeEvent;

    private void Awake()
    {
        config = new();
        countdownTime = float.Parse(config.GetValue("Timer", "terms"));
    }


    void OnEnable()
    {
        currentTime = countdownTime;

        // myToggle.isOn = false;
        // UpdateButtonState(false);
        // myToggle.onValueChanged.AddListener(OnToggleValueChanged);

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

    // void OnToggleValueChanged(bool isChecked)
    // {
    //     UpdateButtonState(isChecked);
    // }

    // void UpdateButtonState(bool isActive)
    // {
    //     myButton.interactable = isActive;

    //     Image buttonImage = myButton.GetComponent<Image>();

    //     Color color = buttonImage.color;
    //     color.a = isActive ? 1f : 220f / 255f;
    //     buttonImage.color = color;
    // }


}
