using System;
using System.Collections;
using UnityEngine;

public class CalendarioPromocoes : MonoBehaviour
{
    public string categoriaSorriso;
    public string cupons;
    public string linkQrCode;
    public string textoVariavelStep2;
    public string qrcodepath;
    public float updateTime = 120;
    private ConfigManager config;


    public readonly (string data, string categoria, string cupons, string link, string texto, string qrcodepath)[] calendario = {
        //teste
        ("19/11", "rindo", "15% off em Cafeteiras 3 Corações", "https://www.amazon.com.br/promotion/psp/A2UXES9WMAZ095?ref=BRXCM24RETAILBFCAFETEIRA", "• 15% off em Cafeteiras 3 Corações\n• ou 20% off em Cozinha","amazon_qrcode_blackfriday_casaecozinhapequeno"),
        ("19/11", "neutro", "30% off em Esportes", "https://www.amazon.com.br/promotion/psp/A25Y2R30GVRA99?ref=psp_pc_cart_collapse?ref=BRXCM24RETAILBFESPORTE", "• 30% off em Esportes\n• ou 30% off em Vestuário Esportivo","amazon_qrcode_blackfriday_esportesneutro"),
        ("19/11", "sorrindo", "Livros Importados na Black Friday com 40% off", "https://www.amazon.com.br/promotion/psp/A26A5T6HUXUVZC?ref=BRXCM24RETAILBFLIVROS", "• 20% off em HQs e Mangás\n• ou Livros Importados na Black Friday com 40% off","amazon_qrcode_blackfriday_livrossorrindo"),

        ("26/11", "rindo", "15% off em Cafeteiras 3 Corações", "https://www.amazon.com.br/promotion/psp/A2UXES9WMAZ095?ref=BRXCM24RETAILBFCAFETEIRA", "• 15% off em Cafeteiras 3 Corações\n• ou 20% off em Cozinha","amazon_qrcode_blackfriday_casaecozinhapequeno"),
        ("26/11", "neutro", "15% off em Cafeteiras 3 Corações", "https://www.amazon.com.br/promotion/psp/A2UXES9WMAZ095?ref=BRXCM24RETAILBFCAFETEIRA", "• 15% off em Cafeteiras 3 Corações\n• ou 20% off em Cozinha","amazon_qrcode_blackfriday_casaecozinhaneutro"),
        ("26/11", "sorrindo", "20% off em Cozinha", "https://www.amazon.com.br/promotion/psp/A3QXN2G1NBZWXI?ref=psp_pc_cart_collapse?ref=BRXCM24RETAILBFCOZINHA1", "• 15% off em Cafeteiras 3 Corações\n• ou 20% off em Cozinha","amazon_qrcode_blackfriday_casaecozinhasorrindo"),

        ("27/11", "rindo", "20% off em Acessórios Esportivos", "https://www.amazon.com.br/promotion/psp/A2ONBP8I52ZG67?ref=psp_pc_cart_collapse?ref=BRXCM24RETAILBFACESSOESPORT", "• 30% off em Esportes\n• ou 30% off em Vestuário Esportivo","amazon_qrcode_blackfriday_esportespequeno"),
        ("27/11", "neutro", "30% off em Esportes", "https://www.amazon.com.br/promotion/psp/A25Y2R30GVRA99?ref=psp_pc_cart_collapse?ref=BRXCM24RETAILBFESPORTE", "• 30% off em Esportes\n• ou 30% off em Vestuário Esportivo","amazon_qrcode_blackfriday_esportesneutro"),
        ("27/11", "sorrindo", "30% off em Vestuário Esportivo", "https://www.amazon.com.br/promotion/psp/A2LT80GXOTQKZZ?ref=BRXCM24RETAILBFVESTESPORT", "• 30% off em Esportes\n• ou 30% off em Vestuário Esportivo","amazon_qrcode_blackfriday_esportessorrindo"),

        ("28/11", "rindo", "15% off em Ferramentas Milwaukee", "https://www.amazon.com.br/promotion/psp/A1P61VIZ91L4V1?ref=psp_pc_cart_collapse?ref=BRXCM24RETAILBFFERRAMENTA", "• 15% off em Ferramentas Milwaukee\n• ou 30% off Casa e Construção","amazon_qrcode_blackfriday_homeeferramentaspequeno"),
        ("28/11", "neutro", "20% off em Cozinha", "https://www.amazon.com.br/promotion/psp/A3QXN2G1NBZWXI?ref=psp_pc_cart_collapse?ref=BRXCM24RETAILBFCOZINHA2", "• 15% off em Ferramentas Milwaukee\n• ou 30% off Casa e Construção","amazon_qrcode_blackfriday_homeeferramentasneutro"),
        ("28/11", "sorrindo", "30% off Casa e Construção", "https://www.amazon.com.br/promotion/psp/A1ZHWRTJL903FE?ref=BRXCM24RETAILBFCASAECONST", "• 15% off em Ferramentas Milwaukee\n• ou 30% off Casa e Construção","amazon_qrcode_blackfriday_homeeferramentassorrindo"),

        ("29/11", "rindo", "20% off em HQs e Mangás", "https://www.amazon.com.br/promotion/psp/A3UPZTFJEDMBZH?ref=BRXCM24RETAILBFHQ", "• 20% off em HQs e Mangás\n• ou Livros Importados na Black Friday com 40% off","amazon_qrcode_blackfriday_livrospequeno"),
        ("29/11", "neutro", "20% off em HQs e Mangás", "https://www.amazon.com.br/promotion/psp/A3UPZTFJEDMBZH?ref=BRXCM24RETAILBFHQ", "• 20% off em HQs e Mangás\n• ou Livros Importados na Black Friday com 40% off","amazon_qrcode_blackfriday_livrosneutro"),
        ("29/11", "sorrindo", "Livros Importados na Black Friday com 40% off", "https://www.amazon.com.br/promotion/psp/A26A5T6HUXUVZC?ref=BRXCM24RETAILBFLIVROS", "• 20% off em HQs e Mangás\n• ou Livros Importados na Black Friday com 40% off","amazon_qrcode_blackfriday_livrossorrindo"),

        ("30/11", "Pequeno", "20% off em Colecionáveis", "https://www.amazon.com.br/promotion/psp/A3VUI9UQU41YUP?ref=BRXCM24RETAILBFCOLECIONAVEIS", "• 20% off em Colecionáveis\n• ou 20% off em Blocos de Montar","amazon_qrcode_blackfriday_brinquedosejogospequeno"),
        ("30/11", "neutro", "20% off em Jogos e Quebra-Cabeças", "https://www.amazon.com.br/promotion/psp/A3A3WHNLLC3NMU?ref=BRXCM24RETAILBFQUEBRACABECA", "• 20% off em Colecionáveis\n• ou 20% off em Blocos de Montar","amazon_qrcode_blackfriday_brinquedosejogosneutro"),
        ("30/11", "sorrindo", "20% off em Blocos de Montar", "https://www.amazon.com.br/promotion/psp/A1250WXSGHE8OL?ref=BRXCM24RETAILBFBLOCOMONTAR", "• 20% off em Colecionáveis\n• ou 20% off em Blocos de Montar","amazon_qrcode_blackfriday_brinquedosejogossorrindo"),

        ("01/12", "rindo", "R$ 200 off em Eletrônicos", "https://www.amazon.com.br/promotion/psp/A1Y9EENLDOCAFW?ref=BRXCM24RETAILBFELETRONICOS", "• R$ 200 off em Eletrônicos\n• ou R$500 off em iPhone 16","amazon_qrcode_blackfriday_eletronicospequeno"),
        ("01/12", "neutro", "R$ 200 off em Eletrônicos", "https://www.amazon.com.br/promotion/psp/A1Y9EENLDOCAFW?ref=BRXCM24RETAILBFELETRONICOS", "• R$ 200 off em Eletrônicos\n• ou R$500 off em iPhone 16","amazon_qrcode_blackfriday_eletronicosneutro"),
        ("01/12", "sorrindo", "R$500 off em iPhone 16", "https://www.amazon.com.br/promotion/psp/A1FV9QEMBNVYPP?ref=psp_pc_cart_collapse?ref=BRXCM24RETAILBFIPHONE16", "• R$ 200 off em Eletrônicos\n• ou R$500 off em iPhone 16","amazon_qrcode_blackfriday_eletronicossorrindo"),

        ("02/12", "rindo", "15% off em cuidados para o cabelo", "https://www.amazon.com.br/promotion/psp/A3UN50N7OKD32B?ref=psp_pc_cart_collapse?ref=BRXCM24RETAILBFCABELO", "• 15% off em cuidados para o cabelo\n• ou 20% off em beleza","amazon_qrcode_blackfriday_belezapequeno"),
        ("02/12", "neutro", "15% off em cuidados para o cabelo", "https://www.amazon.com.br/promotion/psp/A3UN50N7OKD32B?ref=psp_pc_cart_collapse?ref=BRXCM24RETAILBFCABELO", "• 15% off em cuidados para o cabelo\n• ou 20% off em beleza","amazon_qrcode_blackfriday_belezaneutro"),
        ("02/12", "sorrindo", "20% off em beleza", "https://www.amazon.com.br/promotion/psp/AGJFIGLJLYDKY?ref=BRXCM24RETAILBFBELEZA", "• 15% off em cuidados para o cabelo\n• ou 20% off em beleza","amazon_qrcode_blackfriday_belezasorrindo")
    };

    private void Awake()
    {
        config = new();
    }

    private void Start()
    {
        StartCoroutine(AtualizarPromocoes());
    }

    private IEnumerator AtualizarPromocoes()
    {
        while (true)
        {
            AtualizarDados();
            yield return new WaitForSeconds(updateTime);
        }
    }

    private void AtualizarDados()
    {
        Debug.Log("Atualizando dados");
        string hoje = DateTime.Now.ToString("dd/MM");
        foreach (var item in calendario)
        {
            if (item.data == hoje)
            {
                categoriaSorriso = item.categoria;
                cupons = item.cupons;
                linkQrCode = item.link;
                textoVariavelStep2 = item.texto;
                qrcodepath = item.qrcodepath;
                Debug.Log($"Atualizado para o dia {hoje}: {categoriaSorriso}, {cupons}, {linkQrCode}, {textoVariavelStep2}");
                config.SetValue("Calendar", "textoVariavelStep2", textoVariavelStep2);
                config.SetValue("Calendar", "dia", hoje);
                config.SaveConfig();
                break;
            }
            else
            {
                textoVariavelStep2 = "• 15% off em Cafeteiras 3 Corações\n• ou 20% off em Cozinha";
            }
        }
    }

    public (string cupons, string link, string texto, string qrcodepath) BuscarPorCategoriaEData(string categoria, string data)
    {
        foreach (var item in calendario)
        {
            if (item.categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase) && item.data == data)
            {
                return (item.cupons, item.link, item.texto, item.qrcodepath);
            }
        }

        Debug.LogWarning($"Nenhum resultado encontrado para a categoria '{categoria}' na data '{data}'.");
        return ("15% off em Cafeteiras 3 Corações", "https://www.amazon.com.br/promotion/psp/A2UXES9WMAZ095?ref=BRXCM24RETAILBFCAFETEIRA", "• 15% off em Cafeteiras 3 Corações", "amazon_qrcode_blackfriday_casaecozinhapequeno");
    }
}
