using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPersonagem : MonoBehaviour{

    private GameObject controlador;
    [SerializeField] private Vector2 voo = new Vector2(0, 350);

    private bool podeVoar   = true;
    private bool jogando    = true;

    // Start is called before the first frame update
    void Start(){
        controlador = GameObject.Find("Controlador");
    }

    // Update is called once per frame
    void Update(){
        if ( (Input.touchCount > 0 || Input.GetMouseButtonDown(0)) && jogando && podeVoar) {
            if (Input.GetMouseButtonDown(0) || Input.GetTouch(0).phase == TouchPhase.Began) {
                if (!controlador.GetComponent<ControladorJogo>().jogoIniciado) {
                    controlador.GetComponent<ControladorJogo>().iniciarJogo();
                } else {
                    if (this.GetComponent<Rigidbody2D>().isKinematic) {
                        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        voar();
                    }
                }
            }
        }

        if (this.transform.rotation.eulerAngles.z > 30.0f && this.transform.rotation.eulerAngles.z <= 180 && jogando && podeVoar) {
            transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, 30.0f);
        }

        if (this.transform.rotation.eulerAngles.z > 180.0f && this.transform.rotation.eulerAngles.z <= 330 && jogando && podeVoar) {
            transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, -30.0f);
        }

        if (this.GetComponent<Rigidbody2D>().velocity.y < 0 && jogando && podeVoar) {
            this.GetComponent<Rigidbody2D>().rotation -= 1f;
        }

        if (!podeVoar && Mathf.Abs(transform.rotation.eulerAngles.z - 270f) > 0.1f ) {
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, 270.0f);
        }
    }

    private void voar() {
        if (podeVoar) {
            Rigidbody2D passaro = this.GetComponent<Rigidbody2D>();
            passaro.velocity = Vector2.zero;
            if (passaro.rotation <= 0) {
                passaro.rotation = 7.5f;
            }
            passaro.rotation += 7.5f;
            passaro.AddForce(voo);
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (jogando) {
            if (other.gameObject.name == "fundoChao") {
                if (podeVoar) {
                    controlador.GetComponent<ControladorJogo>().pararJogo();
                    this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, 270.0f);
                    this.GetComponent<Animation>().enabled = false;
                }

                this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                jogando = false;
                other.transform.GetComponent<AudioSource>().Play();
                controlador.GetComponent<ControladorPontos>().PreencherGo(); 
            }else if (other.gameObject.tag == "canos" || other.gameObject.tag == "limite") {
                podeVoar = false;
                controlador.GetComponent<ControladorJogo>().pararJogo();

                foreach (Transform child in other.transform.parent) {
                    child.transform.GetComponent<BoxCollider2D>().enabled = false;
                    GetComponent<AudioSource>().Play();
                    this.GetComponent<Animation>().enabled = false;
                    this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, 270.0f);
                }
            }
        }
    }
}
