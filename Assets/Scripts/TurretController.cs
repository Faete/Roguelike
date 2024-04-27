using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Spell spell;
    bool canShoot = true;
    float reloadTime = 0.4f;

    void Update(){
        if(canShoot){
            canShoot = false;
            GameObject projObj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile proj = projObj.GetComponent<Projectile>();
            proj.spell = spell;
            proj.direction = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100)).normalized;
            proj.targetTag = "Player";
            Invoke(nameof(Reload), reloadTime);
        }
    }

    void Reload(){
        canShoot = true;
    }
}
