using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorPontos : MonoBehaviour{

    public int Ponto {
        get;
        private set;
    }

    public Text pontuacaoJogo;
    public Text pontuacaoGameOver;
    public Text pontuacaoRecorde;
    public Image pontuacaoMedalha;

    public Sprite[] medalhas;

    public GameObject telaGameOver;

    private void Awake() {
        // Verificar se existe algum recorde
        if (!PlayerPrefs.HasKey("Recorde")) {
            PlayerPrefs.SetInt("Recorde", 0);
        }

        Ponto = 0;
    }

    public void pontuar() {
        Ponto++;
        pontuacaoJogo.text = Ponto.ToString();

        if (Ponto > PlayerPrefs.GetInt("Recorde")) {
            PlayerPrefs.SetInt("Recorde", Ponto);
        }
    }

    public void PreencherGo() {
        telaGameOver.SetActive(true);
        pontuacaoGameOver.text  = Ponto.ToString();
        pontuacaoRecorde.text   = PlayerPrefs.GetInt("Recorde").ToString();

        if (Ponto >= 3) {
            pontuacaoMedalha.enabled = true;

            int medalha = (Ponto / 3) - 1;
            if (medalha > medalhas.Length - 1) {
                medalha = medalhas.Length - 1;
            }

            pontuacaoMedalha.sprite = medalhas[medalha];
        } else {
            pontuacaoMedalha.enabled = false;
        }
    }
}
