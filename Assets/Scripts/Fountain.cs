using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{
    bool hasBeenUsed = false;
    public Sprite emptySprite;
    private SpriteRenderer sr;

    void Start(){
        sr = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D other){
        if(!hasBeenUsed && other.transform.CompareTag("Player")){
            other.transform.SendMessage("FullRestore");
            sr.sprite = emptySprite;
            hasBeenUsed = true;
        }
    }
}
