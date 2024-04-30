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

    void Save(Savedata savedata){
        savedata.experience = currentExperience;
        savedata.level = currentLevel;
    }

    void Load(Savedata savedata){
        currentExperience = savedata.experience;
        currentLevel = savedata.level;
    }
}
