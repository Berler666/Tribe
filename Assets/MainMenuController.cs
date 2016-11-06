using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public void SinglePlayer()
    {
        SceneManager.LoadScene("Valley River");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
