using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    float currentExperience = 0;
    int currentLevel = 1;
    [SerializeField] GameObject levelUpPanel;

    void GainExp(float exp){
        currentExperience += exp;
        if(currentExperience >= currentLevel * 10){
            currentExperience -= currentLevel * 10;
            ++currentLevel;
            levelUpPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void IncMana(){
        transform.SendMessage("IncreaseMaxMana");
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void IncHealth(){
        transform.SendMessage("IncreaseMaxHealth");
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void IncAttackSpeed(){
        transform.SendMessage("IncreaseAttackSpeed");
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
