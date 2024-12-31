using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoDoJogador : MonoBehaviour
{
    [Header("Referências")]
    private Rigidbody2D oRigidbody2D;
    private Animator oAnimator;

    [Header("Movimento Horizontal")]
    public float velocidadeDoJogador;
    public bool indoParaDireita;

    [Header("Pulo")]
    public bool estaNoChao;
    public float alturaDoPulo;
    public float tamanhoDoRaioDeVerificacao;
    public Transform verificadorDeChao;
    public LayerMask layerDoChao;

    [Header("Wall Jump")]
    public bool estaNaParede;
    public bool estaPulandoNaParede;
    public float forcaXDoWallJump;
    public float forcaYDoWallJump;
    public Transform verificadorDeParede;

    [Header("Verificações ")]
    public bool jogadorEstaVivo;

    void Awake()
    {
        oRigidbody2D = GetComponent<Rigidbody2D>();
        oAnimator = GetComponent<Animator>();
    }


    void Start()
    {
        jogadorEstaVivo = true;
    }

    void Update()
    {
        if(jogadorEstaVivo == true)
        {
            MovimentarJogador();
            Pular();
            WallJump();
        }
    }

    private void MovimentarJogador()
    {
        // Cuida do movimento horizontal do jogador
        float movimentoHorizontal = Input.GetAxis("Horizontal");

        oRigidbody2D.velocity = new Vector2(movimentoHorizontal * velocidadeDoJogador, oRigidbody2D.velocity.y);

       // Espelha o jogador dependendo da sua direção
        if(movimentoHorizontal > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            indoParaDireita = true;
        }
        else if(movimentoHorizontal < 0)
        {
            transform.localScale = new Vector3(-1f, 1f,1f);
            indoParaDireita = false;
        }
        // Toca as animações do jogador parado e andando
        if(movimentoHorizontal == 0 && estaNoChao == true)
        {
            oAnimator.Play("jogador-idle");
        }
        else if(movimentoHorizontal != 0 && estaNoChao == true  && estaNaParede == false)
        {
            oAnimator.Play("jogador-andando");
        }
    }

    private void Pular()
    {
        // Verifica se p jogador esta encostando no chão
        estaNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, tamanhoDoRaioDeVerificacao, layerDoChao);
        
        //faz o jogador pular
        if(Input.GetButtonDown("Jump") && estaNoChao == true)
        {
            SFXManager.instance.somDoPulo.Play();
            oRigidbody2D.AddForce(new Vector2(0f, alturaDoPulo), ForceMode2D.Impulse);
        }
        // Toca a animação do jogador pulando
        if(estaNoChao == false && estaNaParede == false)
        {
            oAnimator.Play("jogador-pulando");
        }
    }
    private void WallJump()
    {
        // Verifica se o jogador esta encostando na parede
        estaNaParede = Physics2D.OverlapCircle(verificadorDeParede.position, tamanhoDoRaioDeVerificacao, layerDoChao);

        //Toca a animaçaõ do jogador deslizando na parede
        if(estaNaParede == true && estaNoChao == false)
        {
            oAnimator.Play("jogador-deslizando-na-parede");
        }

        // O jogador esta na parede e esta pulando
        if(Input.GetButtonDown("Jump") && estaNaParede == true && estaNoChao == false)
        {
            estaPulandoNaParede = true;
        }

        //Faz o jogador pula na parede(ir na dieção oposta dela)
        if(estaPulandoNaParede == true)
        {
            if(indoParaDireita == true)
            {
                oRigidbody2D.velocity = new Vector2(-forcaXDoWallJump, forcaYDoWallJump);
            }
            else
            {
                oRigidbody2D.velocity = new Vector2(forcaXDoWallJump, forcaYDoWallJump);
            }
            
            // Diz para a Unity que o jogador saiu da parede(depois de x segundos)
            Invoke(nameof(DeixarEstaPulandoNaParedeComoFalso), 0.1f);

        }
    }

    private void DeixarEstaPulandoNaParedeComoFalso()
    {
        estaPulandoNaParede = false;
    }
    public void ImpulsionarJogador(float forcaDoImpulso)
    {
        oRigidbody2D.velocity = new Vector2(oRigidbody2D.velocity.x, 0f);
        oRigidbody2D.AddForce(new Vector2(0f, forcaDoImpulso), ForceMode2D.Impulse);
    }




}
