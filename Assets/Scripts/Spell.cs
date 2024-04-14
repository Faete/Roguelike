using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Spell : ScriptableObject
{
    public string spellName;
    public string description;
    public RuntimeAnimatorController animationController;
    public Sprite icon;
    public string spellType;
    public float projectileSpeed;
    public float power;
    public float manaCost;
    
    /* 
        0 -> Sleep
        1 -> Poison
        2 -> Paralysis
    */
    public int[] statusArray = new int[3];
    
}
