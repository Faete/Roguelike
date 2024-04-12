using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    public string targetTag;
    public float power;

    void Start(){
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Wall") || other.CompareTag(targetTag)) Destroy(gameObject);
        if(other.CompareTag(targetTag)){
            other.SendMessage("TakeDamage", power);
        }
    }
}
