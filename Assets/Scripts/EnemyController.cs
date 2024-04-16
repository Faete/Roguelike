using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyController : MonoBehaviour
{
    public float health;
    public float moveSpeed = 2f;
    public float attackRange = 5f;
    public Spell spell;
    public GameObject projectilePrefab;
    Transform playerTransform;

    private AIPath aiPath;
    private Rigidbody2D rb;
    private Animator animator;

    public float reloadTime = 0.5f;
    bool canShoot = true;

    void Start(){
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        aiPath = GetComponent<AIPath>();
        animator = GetComponent<Animator>();
        aiPath.maxSpeed = moveSpeed;
        aiPath.endReachedDistance = attackRange;
    }

    void Update(){
        AnimationControls();
        FacingDir();
        AttackBehavior();
    }

    void TakeDamage(float damage){
        health -= damage;
        if(health <= 0) Die();
    }

    void Die(){
        Destroy(gameObject);
    }

    void AnimationControls(){
        if(aiPath.desiredVelocity != Vector3.zero) animator.SetBool("isRunning", true);
        else animator.SetBool("isRunning", false);
    }

    void AttackBehavior(){
        if(Vector3.Distance(transform.position, playerTransform.position) <= attackRange && canShoot){
            canShoot = false;
            Vector2 direction = ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;
            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile projectile = proj.GetComponent<Projectile>();
            projectile.spell = spell;
            projectile.direction = direction;
            projectile.targetTag = "Player";
            Invoke(nameof(Reload), reloadTime);
        }
    }

    void Reload(){
        canShoot = true;
    }


    void FacingDir(){
        if(aiPath.desiredVelocity.x >= 0) transform.localScale = new Vector3(1f, 1f, 1f);
        else transform.localScale = new Vector3(-1f, 1f, 1f);
    }

}
