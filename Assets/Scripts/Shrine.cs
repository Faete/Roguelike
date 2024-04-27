using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    bool hasBeenUsed = false;
    public Sprite emptySprite;
    private SpriteRenderer sr;
    private GameObject upgradePanel;

    void Start(){
        sr = GetComponent<SpriteRenderer>();
        upgradePanel = FindObjectOfType<Canvas>().transform.GetChild(2).gameObject;
    }

    void OnCollisionEnter2D(Collision2D other){
        if(!hasBeenUsed && other.transform.CompareTag("Player")){
            Time.timeScale = 0f;
            upgradePanel.SetActive(true);
            sr.sprite = emptySprite;
            hasBeenUsed = true;
        }
    }
}
