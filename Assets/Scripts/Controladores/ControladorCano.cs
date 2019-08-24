using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCano : MonoBehaviour{
    private ControladorJogo controlador;
    private bool pontuado = false;
    // Start is called before the first frame update
    void Start(){
        controlador = GameObject.Find("Controlador").GetComponent<ControladorJogo>();
        transform.position = new Vector3(transform.position.x, Random.Range(0, 2.75f) - 1.5f, transform.position.z);
        GetComponent<Rigidbody2D>().velocity = new Vector2(controlador.velocidadeCanos ,0);
    }

    // Update is called once per frame
    void Update(){
        if (this.transform.position.x < -7 && !pontuado) {
            controlador.GetComponent<ControladorPontos>().pontuar();
            pontuado = true;
            controlador.GetComponent<AudioSource>().Play();
        }

        if (this.transform.position.x < -8) {
            GameObject.Destroy(this.gameObject);
        }

        if (!controlador.jogoIniciado) {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
