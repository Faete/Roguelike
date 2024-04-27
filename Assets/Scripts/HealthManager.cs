using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    float maxHealth = 10;
    public float currentHealth;

    public GameObject healthBar;
    public RectTransform healthBarImage;
    private float healthBarWidth;

    [SerializeField] GameObject deathPanel;

    void Start(){
        currentHealth = maxHealth;
        healthBarImage = healthBar.GetComponent<RectTransform>();
        healthBarWidth = healthBarImage.rect.width;
    }

    void Update(){
        healthBarImage.sizeDelta = new Vector2((currentHealth / maxHealth) * healthBarWidth, healthBarImage.rect.height);
    }

    void TakeDamage(float damage){
        currentHealth -= damage;
        if(currentHealth <= 0f) Die();
    }

    public void Heal(float healAmount){
        currentHealth += healAmount;
        if(currentHealth > maxHealth) currentHealth = maxHealth;
    }

    void Die(){
        Time.timeScale = 0f;
        healthBarImage.sizeDelta = Vector2.zero;
        deathPanel.SetActive(true);
    }

    public void IncreaseMaxHealth(){
        maxHealth += 5f;
        Heal(5f);
    }

    void FullRestore(){
        currentHealth = maxHealth;
    }
}
