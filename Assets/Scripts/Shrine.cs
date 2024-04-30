using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    bool hasBeenUsed = false;
    public Sprite emptySprite;
    private SpriteRenderer sr;
    private GameObject upgradePanel;
    private BoxCollider2D bc;

    void Start(){
        sr = GetComponent<SpriteRenderer>();
        upgradePanel = FindObjectOfType<Canvas>().transform.GetChild(2).gameObject;
        bc = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D other){
        if(!hasBeenUsed && other.transform.CompareTag("Player")){
            Time.timeScale = 0f;
            upgradePanel.SetActive(true);
            sr.sprite = emptySprite;
            hasBeenUsed = true;
            bc.offset = new Vector2(0f, -0.2f);
            bc.size = new Vector2(1f, 1.6f);
        }
    }
}
