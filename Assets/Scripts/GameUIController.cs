using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject buildPanel;

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
        buildPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void BuildPanelClose()
    {
        buildPanel.SetActive(false);
        Time.timeScale = 1;
    }


    // Build Menu //



    public void BuildHut()
    {
        buildPanel.SetActive(false);
        instanciateobjectonclick.hut = true;
        Time.timeScale = 1;
    }

    public void BuildGatheringHut()
    {
        buildPanel.SetActive(false);
        instanciateobjectonclick.gatheringhut = true;
        Time.timeScale = 1;
    }

    public void BuildChiefHut()
    {
        buildPanel.SetActive(false);
        instanciateobjectonclick.chiefhut = true;
        Time.timeScale = 1;
    }





    // Butts
}
