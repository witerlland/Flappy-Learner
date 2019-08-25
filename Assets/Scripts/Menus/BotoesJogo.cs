using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BotoesJogo : MonoBehaviour{
    public void jogar(){
        // SceneManagement.LoadScene("Jogo");
        SceneManager.LoadScene("sceneJogo");
    }

    public void menu(){
        SceneManager.LoadScene("sceneMenu");
    }

}
