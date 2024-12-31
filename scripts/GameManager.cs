using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string nomeDaProximaFase;
    public float tempoParaRecarregarFase;
    public float tempoParaCarregarNovaFase;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        RodarCoroutineRecarregarFase();
    }

    public void RodarCoroutineRecarregarFase()
    {
        StartCoroutine(RecarregarFase());
    }

    private IEnumerator RecarregarFase()
    {
        yield return new WaitForSeconds(tempoParaRecarregarFase);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RodarCoroutinePassarDeFase()
    {
        StartCoroutine(PassarDeFase());
    }

    private IEnumerator PassarDeFase()
    {
        yield return new WaitForSeconds(tempoParaCarregarNovaFase);
        SceneManager.LoadScene(nomeDaProximaFase);
    }
}
