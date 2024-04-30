using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] Savedata defaultSavedata;

    public void BackFromTutorial(){
        tutorialPanel.SetActive(false);
        buttons.SetActive(true);
    }

    public void NewRun(){
        if(!PlayerPrefs.HasKey("Volume")) PlayerPrefs.SetFloat("Volume", 1f);
        File.WriteAllText(Application.persistentDataPath + "/savedata.json", JsonUtility.ToJson(defaultSavedata));
        SceneManager.LoadScene("PlayScene");
    }

    public void ContinueRun(){
        if(!File.Exists(Application.persistentDataPath + "/savedata.json")) return;
        else SceneManager.LoadScene("PlayScene");
    }

    public void Tutorial(){
        buttons.SetActive(false);
        tutorialPanel.SetActive(true);
    }

    public void Quit(){
        Application.Quit();
    }
}
