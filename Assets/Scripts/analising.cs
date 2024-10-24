using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class analising : MonoBehaviour
{
    public VideoPlayer player;
    [SerializeField] private GameObject fitas_qr;

    void Start()
    {
        // Registra o método para ser chamado quando o vídeo terminar
        player.loopPointReached += OnVideoEnd;
    }

    private void OnEnable()
    {
        player.Play();
    }


    void OnVideoEnd(VideoPlayer player)
    {
        fitas_qr.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        player.time = 0;
    }

}
