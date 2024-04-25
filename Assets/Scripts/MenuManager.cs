using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void NewRun(){
        SceneManager.LoadScene("PlayScene");
    }

    public void ContinueRun(){
        if(!File.Exists(Application.persistentDataPath + "/savedata.json")) return;
    }

    public void Tutorial(){
        SceneManager.LoadScene("Tutorial");
    }

    public void Quit(){
        Application.Quit();
    }
}
