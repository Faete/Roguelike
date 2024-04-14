using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Spell spell;
    public string targetTag;
    private Animator animator;
    private CircleCollider2D cc;

    void Start(){
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = spell.animationController;
        cc = GetComponent<CircleCollider2D>();
    }

    public void TurnOnCollider(){
        GetComponent<Collider2D>().enabled = true;
    }

    public void End(){
        Destroy(gameObject);
    }

    public void SetColliderRadius(float radius){
        cc.radius = radius;
    }

    public void SetColliderOffset(float offsetx){
        cc.offset = new Vector2(offsetx, cc.offset.y);
    }

    public void SetColliderOffsetY(float offsety){
        cc.offset = new Vector2(cc.offset.x, offsety);
    }
    
    public void TurnOffCollider(){
        cc.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag(targetTag)) other.SendMessage("TakeDamage", spell.power);
    }
}