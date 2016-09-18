using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RTSUIController : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject buildPanel;

    public static bool buildMenuOpen = false;

    //Resources

    public static int Money = 5000;
    public static int Wood = 1000;

    public Text moneyText;
    public Text woodText;

    // Use this for initialization
    void Start () {

        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        buildPanel.SetActive(false);

       
	
	}
	
	// Update is called once per frame
	void Update () {
        moneyText.GetComponent<Text>();
        moneyText.text = "Gold: " + Money;

        woodText.GetComponent<Text>();
        woodText.text = "Wood: " + Wood;
    }

    public void PauseButton()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void UnPauseButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BuildPanelOpen()
    {
        BuildMenu.inMenu = true;
        buildPanel.SetActive(true);
        buildMenuOpen = true;

        
    }

    public void BuildPanelClose()
    {
        buildPanel.SetActive(false);
        buildMenuOpen = false;
        BuildMenu.ghostActive = false;
        
    }

}
