using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scanning : MonoBehaviour
{
    [SerializeField] private ScreenChangeEvent screenChangeEvent;


    public RawImage webcamTexture;

    private void Update()
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
