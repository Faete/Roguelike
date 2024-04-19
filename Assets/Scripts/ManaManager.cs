using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    float maxMana = 100f;
    public float currentMana;
    float manaRegenTime = 1f;
    private float currentTime;
    float manaRegenRate = 5f;

    public GameObject manaBar;
    public RectTransform manaBarImage;
    private float manaBarWidth;

    void Start(){
        currentMana = maxMana;
        manaBarImage = manaBar.GetComponent<RectTransform>();
        currentTime = 0f;
        manaBarWidth = manaBarImage.rect.width;
    }

    void Update(){
        manaBarImage.sizeDelta = new Vector2((currentMana / maxMana) * manaBarWidth, manaBarImage.rect.height);
    }

    public void AttemptSpell(Spell spell){
        if(currentMana >= spell.manaCost){
            currentMana -= spell.manaCost;
            SendMessage("CastSpell", spell);
        } else SendMessage("SpellFailed");
    }

    public void RunManaRegeneration(){
        currentTime += Time.deltaTime;
        if(currentTime >= manaRegenTime){
            currentTime = 0f;
            RegenMana();
        }
    }

    void RegenMana(){
        float tmpMana = currentMana + manaRegenRate;
        if(tmpMana <= maxMana) currentMana = tmpMana;
        else currentMana = maxMana;
    }

    public void IncreaseMaxMana(){
        maxMana += 20f;
    }
}
