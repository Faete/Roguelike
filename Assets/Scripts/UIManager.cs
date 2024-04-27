using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    public void MainMenu(){
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
}
