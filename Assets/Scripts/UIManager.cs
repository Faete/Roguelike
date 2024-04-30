using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject levelUpPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject pauseButtons;
    [SerializeField] private GameObject volumeInfo;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private MapManager mapManager;
    [SerializeField] private Transform playerTransform;
    bool isPaused = false;

    void Update(){
        Pausing();
    }

    void Pausing(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!isPaused){
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
                volumeInfo.SetActive(false);
                pauseButtons.SetActive(true);
                isPaused = true;
            }
            else Unpause();
        }
    }

    public void Unpause(){
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        isPaused = false;
    }

    public void Volume(){
        pauseButtons.SetActive(false);
        volumeInfo.SetActive(true);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }

    public void VolumeBack(){
        pauseButtons.SetActive(true);
        volumeInfo.SetActive(false);
    }

    public void VolumeChange(){
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();
    }

    public void MainMenuOther(){
        Time.timeScale = 1f;
        winPanel.SetActive(false);
        File.Delete(Application.persistentDataPath + "/savedata");
        SceneManager.LoadScene("Menu");
    }

    public void MainMenuPause(){
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        string json = JsonUtility.ToJson(mapManager.savedata);
        File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
        SceneManager.LoadScene("Menu");
    }

    public void IncMana(){
        playerTransform.SendMessage("IncreaseMaxMana");
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void IncHealth(){
        playerTransform.SendMessage("IncreaseMaxHealth");
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void IncAttack(){
        playerTransform.SendMessage("IncreaseAttackSpeed");
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void IncManaRegen(){
        playerTransform.SendMessage("IncreateManaRegenRate");
        Time.timeScale = 1f;
        upgradePanel.SetActive(false);
    }

    public void IncLifesteal(){
        playerTransform.SendMessage("IncreaseLifesteal");
        Time.timeScale = 1f;
        upgradePanel.SetActive(false);
    }

    public void IncSpellPower(){
        playerTransform.SendMessage("IncreaseSpellPower");
        Time.timeScale = 1f;
        upgradePanel.SetActive(false);
    }
}