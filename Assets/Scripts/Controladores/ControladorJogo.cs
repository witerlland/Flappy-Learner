using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJogo : MonoBehaviour{
    public bool jogoIniciado;

    public GameObject inicial;
    public GameObject[] canos;
    public GameObject[] personagens;
    public GameObject[] fundos;

    public Material[] dia;
    public Material[] noite;

    public float velocidadeCanos = -2.0f;

    private int periodo;
    private GameObject cano;
    void Awake() {
        periodo = Random.Range(0, 2); // 0 = dia, 1= noite
        cano = canos[periodo];

        if (periodo == 1) {
            for (int i = 0; i < noite.Length; i++) {
                fundos[i].GetComponent<Renderer>().material = noite[i];
            }
        } else {
            for (int i = 0; i < dia.Length; i++) {
                fundos[i].GetComponent<Renderer>().material = dia[i];
            }
        }

        int rand = Random.Range(0, personagens.Length);
        GameObject personagem = Instantiate(personagens[rand]);

        jogoIniciado = false;
    }

    public void iniciarJogo() {
        jogoIniciado        = true;
        InvokeRepeating("invocaCanos", 2f, Random.Range(1.8f, 2.5f));
        inicial.SetActive(false);
    }

    public void pararJogo() {
        jogoIniciado = false;

        foreach (GameObject fundo in fundos) {
            fundo.GetComponent<Parallax>().Velocidade = 0f;
        }

        CancelInvoke();
    }

    public void invocaCanos() {
        // Invoca os canos
        Instantiate(cano);
    }
}
