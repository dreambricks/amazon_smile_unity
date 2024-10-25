using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QrCodeScreen : MonoBehaviour
{
    [SerializeField] private ScreenChangeEvent screenChangeEvent;

    public float time;

    private void OnEnable()
    {
        Invoke(nameof(GoToCTA), time);
    }

    void GoToCTA()
    {
        screenChangeEvent.OnScreenChange(ScreenType.CTA);
        gameObject.SetActive(false);
    }
}
