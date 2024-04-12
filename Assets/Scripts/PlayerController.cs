using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Vector2 direction;
    private Rigidbody2D rb;

    [SerializeField] GameObject projectilePrefab;
    float reloadTime = 0.2f;
    bool canShoot = true;

    public float health;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        health = 10f;
    }

    void FixedUpdate(){
        Movement();
        Shoot();
    }

    void Movement(){
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.velocity = direction * moveSpeed;
    }

    void Shoot(){
        if(Input.GetMouseButton(0)){
            if(canShoot){
                canShoot = false;
                Vector2 center = transform.position + new Vector3(0f, -0.5f, 0f);
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = (mousePos - center).normalized;
                GameObject proj = Instantiate(projectilePrefab, center, Quaternion.identity);
                proj.GetComponent<Projectile>().direction = direction;
                proj.GetComponentInChildren<Projectile>().targetTag = "Enemy";
                Invoke(nameof(Reload), reloadTime);
            }
        }
    }

    void Reload(){
        canShoot = true;
    }

    void TakeDamage(float damage){
        health -= damage;
        if(health <= 0f) Die();
    }

    void Die(){
        Destroy(gameObject);
    }
}
