using UnityEngine;
using UnityEngine.UI;
using ZXing;
using System.IO;

public class QrCodeScreen : MonoBehaviour
{
    [SerializeField] private ScreenChangeEvent screenChangeEvent;
    private ConfigManager config;

    public CalendarioPromocoes calendar;

    public Text text;

    public float time;

    public string amazonlink;

    public string qrcodepath;

    public RawImage qrCodeImage;

    private void Awake()
    {
        config = new();
        time = int.Parse(config.GetValue("Timer", "qrcode"));
    }

    private void OnEnable()
    {
        GetCupom();
        Invoke(nameof(GoToCTA), time);
    }

    void GetCupom()
    {
        string emotionSaved = PlayerPrefs.GetString("emotion");
        string today = config.GetValue("Calendar", "dia");
        var calendarValues = calendar.BuscarPorCategoriaEData(emotionSaved, today);
        text.text = calendarValues.cupons;
        amazonlink = calendarValues.link;
        qrcodepath = calendarValues.qrcodepath;

        Debug.Log("qrcodePath: " + qrcodepath);
        string imagesPath = Path.Combine(Application.streamingAssetsPath, "amazonqrcodes");
        Debug.Log("imagesPath: " + imagesPath);
        string folderPath = Path.Combine(imagesPath, qrcodepath);
        Debug.Log("folderPath: " + folderPath);

        string qrcodePath = FindFirstPng(folderPath);
        Debug.Log("qrcodeFullPath: " + qrcodePath);
        LoadPngIntoRawImage(qrcodePath);
    }

    void GoToCTA()
    {
        SaveLog();
        screenChangeEvent.OnScreenChange(ScreenType.CTA);
        PlayerPrefs.DeleteAll();
        gameObject.SetActive(false);
    }

    void SaveLog()
    {
        DataLog dataLog = LogUtil.GetDatalogFromJson();
        dataLog.status = StatusEnum.ACAO_CONCLUIDA.ToString();
        dataLog.additional = PlayerPrefs.GetString("emotion");
        if (dataLog.additional == "rindo")
        {
            dataLog.additional = "pequeno";
        }
        LogUtil.SaveLog(dataLog);
    }

    void GenerateQRCode(string text)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new ZXing.Common.EncodingOptions
            {
                Width = 256,
                Height = 256,
                Margin = 1
            }
        };

        var pixelData = writer.Write(text);

        var texture = new Texture2D(256, 256);
        texture.SetPixels32(pixelData);
        texture.Apply();

        qrCodeImage.texture = texture;
    }

    string FindFirstPng(string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath, "*.png");
            if (files.Length > 0)
            {
                Debug.Log("QR Code File: " + files[0]);
                return files[0];
            }
        }
        else
        {
            Debug.LogError("A pasta especificada não existe: " + folderPath);
        }

        return null;
    }

    void LoadPngIntoRawImage(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);

        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(fileData))
        {
            qrCodeImage.texture = texture;
        }
        else
        {
            Debug.LogError("Falha ao carregar a imagem em Texture2D.");
        }
    }

}
