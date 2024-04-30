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
    float reloadTime = 0.5f;
    bool canShoot = true;

    public ManaManager manaManager;
    public Spell fireBall;
    public Spell spell2;
    public Spell defaultSpell;

    float spellPower = 1f;
    float lifesteal = 0f;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Save(Savedata savedata){
        savedata.spellPower = spellPower;
        savedata.lifesteal = lifesteal;
        savedata.reloadTime = reloadTime;
    }

    void Load(Savedata savedata){
        spellPower = savedata.spellPower;
        lifesteal = savedata.lifesteal;
        reloadTime = savedata.reloadTime;
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
        if(Input.GetMouseButtonDown(1)) manaManager.AttemptSpell(fireBall);
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
                manaManager.AttemptSpell(defaultSpell);
                Invoke(nameof(Reload), reloadTime);
            }
        }
    }

    void Reload(){
        canShoot = true;
    }
    
    void CastSpell(Spell spell){
        if(spell.spellType == "Projectile") CastProjectile(spell);
        else if(spell.spellType == "Explosion") CastExplosion(spell);
    }
    void CastProjectile(Spell spell){
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = proj.GetComponent<Projectile>();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
        projectile.spell = spell;
        projectile.lifesteal = lifesteal;
        projectile.dmgMod = spellPower;
        projectile.direction = direction;
        projectile.targetTag = "Enemy";
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

    public void IncreaseAttackSpeed(){
        reloadTime *= 0.9f;
    }

    public void IncreaseSpellPower(){
        spellPower += 0.5f;
    }

    public void IncreaseLifesteal(){
        lifesteal += 0.2f;
    }

    public void IncreateManaRegenRate(){
        manaManager.IncreaseManaRegenRate();
    }

}
