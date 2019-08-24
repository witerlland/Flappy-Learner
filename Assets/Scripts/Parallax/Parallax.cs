using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour{

    public float Velocidade = 0;
    
    // Update is called once per frame
    void Update(){
        Material material = this.gameObject.GetComponent<Renderer>().material;

        float posicao = material.mainTextureOffset.x + Velocidade;
        material.mainTextureOffset = new Vector2(posicao, material.mainTextureOffset.y);
    }
}
