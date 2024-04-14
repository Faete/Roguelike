using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health;
    Transform playerTransform;

    void Start(){
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void TakeDamage(float damage){
        health -= damage;
        if(health <= 0) Die();
    }

    void Die(){
        Destroy(gameObject);
    }

    void HitStatus(bool[] statusArray){
        
    }
}
