using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject InstrutionPanel;
    public GameObject LoadingPanel;
    public void Start()
    {
        InstrutionPanel.gameObject.SetActive(false);
        LoadingPanel.gameObject.SetActive(false);
    }
    public void Play()
    {
        MainMenuPanel.gameObject.SetActive(false);
        InstrutionPanel.gameObject.SetActive(false);
        LoadingPanel.gameObject.SetActive(true);
        SceneManager.LoadScene("GameLevel");
    }
    public void OpenInstruction()
    {
        MainMenuPanel.gameObject.SetActive(false);
        InstrutionPanel.gameObject.SetActive(true);
    }
    public void BackToMenu()
    {
        InstrutionPanel.gameObject.SetActive(false);
        MainMenuPanel.gameObject.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
