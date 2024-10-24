using UnityEngine;

public class AnimacaoReversaController : MonoBehaviour
{
    private Animator animator;
    private bool animacaoNormalConcluida = false;
    private bool tocandoReverso = false;
    private float tempoAtual = 1f; // Começa do fim da animação
    private float duracaoAnimacao = 2f; // Duração da animação (em segundos)

    // Chamado toda vez que o GameObject é habilitado
    void OnEnable()
    {
        Debug.Log("GameObject habilitado. Reiniciando ciclo de animação.");
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator não encontrado! Certifique-se de que o GameObject tem um Animator Component.");
            return;
        }

        // Reinicia variáveis
        animacaoNormalConcluida = false;
        tocandoReverso = false;
        tempoAtual = 1f; // Reseta para o fim da animação para a reprodução reversa

        animator.enabled = true; // Garante que o Animator esteja ativo
        TocarAnimacaoNormal(); // Inicia a animação normal
    }

    void Update()
    {
        // Verifica se a animação normal terminou e inicia a reversa
        if (!animacaoNormalConcluida && AnimacaoTerminou("tapes"))
        {
            animacaoNormalConcluida = true;
            Debug.Log("Animação normal concluída. Iniciando reverso.");
            tocandoReverso = true;
        }

        // Controla a reprodução reversa
        if (tocandoReverso)
        {
            tempoAtual -= Time.deltaTime / duracaoAnimacao; // Retrocede no tempo

            if (tempoAtual <= 0)
            {
                tempoAtual = 0;
                tocandoReverso = false;
                Debug.Log("Animação reversa concluída. Executando ação final.");
                PararAnimacao(); // Para o Animator
                AcaoFinal(); // Executa a ação final
            }
            else
            {
                animator.Play("tapes", 0, tempoAtual); // Define o ponto atual na animação
            }
        }
    }

    void TocarAnimacaoNormal()
    {
        Debug.Log("Tocando animação normal.");
        animator.speed = 1;
        animator.Play("tapes", 0, 0); // Começa do início
    }

    void PararAnimacao()
    {
        Debug.Log("Parando Animator.");
        animator.enabled = false; // Desativa o Animator para garantir que ele pare
    }

    bool AnimacaoTerminou(string nomeAnimacao)
    {
        AnimatorStateInfo estadoAtual = animator.GetCurrentAnimatorStateInfo(0);
        bool terminou = estadoAtual.IsName(nomeAnimacao) && estadoAtual.normalizedTime >= 1;
        return terminou;
    }

    void AcaoFinal()
    {
        Debug.Log("Ação final executada após a animação reversa.");
        // Coloque aqui a ação desejada após a animação reversa
    }
}
