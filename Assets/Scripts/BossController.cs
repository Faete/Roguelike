using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Pathfinding;
using System;

public class BossController : MonoBehaviour
{
    public Enemy enemy;
    float health;
    public float moveSpeed = 2f;
    public float attackRange = 5f;
    public GameObject projectilePrefab;
    public GameObject explosionPrefab;
    public Spell explosionSpell;
    Transform playerTransform;

    private AIPath aiPath;
    private AIDestinationSetter aiDestinationSetter;
    private Rigidbody2D rb;
    private Animator animator;

    public float reloadTime = 0.5f;
    bool canShoot = true;

    private int phase = 0;
    [SerializeField] List<GameObject> turrets;


    void Start(){
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        aiPath = GetComponent<AIPath>();
        animator = GetComponent<Animator>();

        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        aiPath.maxSpeed = moveSpeed;
        aiPath.endReachedDistance = attackRange;
        aiDestinationSetter.target = playerTransform;

        health = enemy.health;
        animator.runtimeAnimatorController = enemy.animationController;
    }

    void Update(){
        AnimationControls();
        FacingDir();
        AttackBehavior();
    }

    void TakeDamage(float damage){
        health -= damage;
        if(health <= 0) NextPhase();
    }

    void NextPhase(){
        phase += 1;
        if(phase == 1){
            foreach(GameObject turret in turrets) turret.SetActive(true);
            health = enemy.health;
        }
        if(phase == 2) Die();
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
            projectile.spell = enemy.spell;
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

    void CastExplosions(List<Vector3> locations){
        foreach(Vector3 location in locations){
            GameObject explosionObj = Instantiate(explosionPrefab, location, Quaternion.identity);
            Explosion explosion = explosionObj.GetComponent<Explosion>();
            explosion.spell = explosionSpell;
            explosion.targetTag = "Player";
        }
    }

}
