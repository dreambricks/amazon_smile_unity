using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class QrCodeScreen : MonoBehaviour
{
    [SerializeField] private ScreenChangeEvent screenChangeEvent;
    private ConfigManager config;

    public CalendarioPromocoes calendar;

    public Text text;

    public float time;

    public string amazonlink;

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
        GenerateQRCode(amazonlink);
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

}
