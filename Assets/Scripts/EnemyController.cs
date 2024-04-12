using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health;
    Transform playerTransform;

    void TakeDamage(float damage){
        health -= damage;
        if(health <= 0) Die();
    }

    void Die(){
        Destroy(gameObject);
    }
}
