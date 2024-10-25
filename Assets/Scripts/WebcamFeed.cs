using UnityEngine;
using UnityEngine.UI;

public class WebcamFeed : MonoBehaviour
{
    public RawImage rawImage;
    public int cameraIndex = 10;
    private WebCamTexture webcamTexture;

    private ConfigManager config;

    private void Awake()
    {
        config = new();

        int deviceIndex = int.Parse(config.GetValue("Cam", "device", "0"));
        cameraIndex = deviceIndex;
    }

    void Start()
    {
        StartWebcam(cameraIndex);
    }

    public void StartWebcam(int index)
    {
        if (webcamTexture != null)
        {
            webcamTexture.Stop();
            Destroy(webcamTexture);
        }

        if (WebCamTexture.devices.Length > 0 && index < WebCamTexture.devices.Length)
        {
            webcamTexture = new WebCamTexture(WebCamTexture.devices[index].name);
            rawImage.texture = webcamTexture;
            webcamTexture.Play();
        }
        else
        {
            Debug.LogError("Índice de câmera inválido ou nenhuma câmera disponível.");
        }
    }

    void OnDisable()
    {
        if (webcamTexture != null)
        {
            webcamTexture.Stop();
        }
    }

    void OnValidate()
    {
        StartWebcam(cameraIndex);
    }
}
