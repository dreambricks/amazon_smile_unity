using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scanning : MonoBehaviour
{
    [SerializeField] private GameObject analising;

    public RawImage webcamTexture;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            analising.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }


    private void OnDisable()
    {
        webcamTexture.GetComponent<RawImage>().enabled = false;
    }
}
