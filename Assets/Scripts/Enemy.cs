using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    public int health;
    public Spell spell;
    public int experienceGranted;
    public RuntimeAnimatorController animationController;
}
