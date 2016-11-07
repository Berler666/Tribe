using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public GameObject singlePlayerMenu;
    public GameObject multiplayerMenu;
    public GameObject loadGameMenu;
    public GameObject mainMenuOptionsMenu;

    
    void Start()
    {
        singlePlayerMenu.SetActive(false);
        multiplayerMenu.SetActive(false);
        loadGameMenu.SetActive(false);
        mainMenuOptionsMenu.SetActive(false);
    }

    public void SinglePlayer()
    {
        multiplayerMenu.SetActive(false);
        loadGameMenu.SetActive(false);
        mainMenuOptionsMenu.SetActive(false);
        singlePlayerMenu.SetActive(true);
    }

    public void Multiplayer()
    {
        singlePlayerMenu.SetActive(false);
        loadGameMenu.SetActive(false);
        mainMenuOptionsMenu.SetActive(false);
        multiplayerMenu.SetActive(true);
    }

    public void LoadGame()
    {
        singlePlayerMenu.SetActive(false);
        multiplayerMenu.SetActive(false);
        mainMenuOptionsMenu.SetActive(false);
        loadGameMenu.SetActive(true);
    }

    public void Options()
    {
        singlePlayerMenu.SetActive(false);
        multiplayerMenu.SetActive(false);
        loadGameMenu.SetActive(false);
        mainMenuOptionsMenu.SetActive(true);
    }

    public void Skirmish()
    {
        SceneManager.LoadScene("Valley River");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
