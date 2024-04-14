using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Vector2 direction;
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject explosionPrefab;
    float reloadTime = 0.2f;
    bool canShoot = true;

    float maxHealth = 10f;
    public float health;

    public ManaManager manaManager;
    public Spell spell1;
    public Spell spell2;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate(){
        Movement();
    }

    void Update(){
        Spellcasting();
        Shoot();
        manaManager.RunManaRegeneration();
    }

    void Spellcasting(){
        if(Input.GetKeyDown(KeyCode.Q)) manaManager.AttemptSpell(spell1);
        if(Input.GetKeyDown(KeyCode.E)) manaManager.AttemptSpell(spell2);
    }

    void Movement(){
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.velocity = direction * moveSpeed;
        if(direction.magnitude > 1) direction.Normalize();
        if(transform.localScale.x * direction.x < 0) transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
        if(direction != Vector2.zero) animator.SetBool("isRunning", true);
        else animator.SetBool("isRunning", false);
    }

    void Shoot(){
        if(Input.GetMouseButton(0)){
            if(canShoot){
                canShoot = false;
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
                GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
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
    
    void CastSpell(Spell spell){
        if(spell.spellType == "Projectile") CastProjectile(spell);
        else if(spell.spellType == "Explosion") CastExplosion(spell);
    }
    void CastProjectile(Spell spell){
        GameObject proj = Instantiate(projectilePrefab);
        Projectile projectile = proj.GetComponent<Projectile>();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
        projectile.direction = direction;
        projectile.power = spell.power;
    }

    void CastExplosion(Spell spell){
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject explosionObj = Instantiate(explosionPrefab, mousePos, Quaternion.identity);
        Explosion explosion = explosionObj.GetComponent<Explosion>();
        explosion.spell = spell;
        explosion.targetTag = "Enemy";
    }

    void SpellFailed(){
        return;
    }
}
