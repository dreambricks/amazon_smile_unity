using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prepare : MonoBehaviour
{
    [SerializeField] private GameObject scanning;
    public RawImage webcamTexture;

    private void OnEnable()
    {
        webcamTexture.GetComponent<RawImage>().enabled = true;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            scanning.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}

