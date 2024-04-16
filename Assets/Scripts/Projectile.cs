using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    public string targetTag;
    public Spell spell;


    private Animator animator;
    private Rigidbody2D rb;
    private CircleCollider2D cc;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
        transform.eulerAngles = new Vector3(0f, 0f, Vector2.SignedAngle(Vector2.right, direction));
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = spell.animationController;
        cc = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Wall") || other.CompareTag(targetTag)){
            GetComponent<CircleCollider2D>().enabled = false;
            rb.velocity = Vector2.zero;
            animator.SetBool("isHit", true); 
        }
        if(other.CompareTag(targetTag)){
            other.SendMessage("TakeDamage", spell.power);
        }
    }

    public void End(){
        Destroy(gameObject);
    }

    public void SetColliderRadius(float radius){
        cc.radius = radius;
    }

    public void SetColliderOffsetX(float offsetX){
        cc.offset = new Vector2(offsetX, cc.offset.y);
    }

    public void SetColliderOffsetY(float offsetY){
        cc.offset = new Vector2(cc.offset.x, offsetY);
    }
}
