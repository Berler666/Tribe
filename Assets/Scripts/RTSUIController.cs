using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RTSUIController : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject buildPanel;
    public GameObject optionsPanel;

    public GameObject devoptions;

    GameObject fpsCanvas;
    GameObject rtsCanvas;
    GameObject RTSCamera;

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
        optionsPanel.SetActive(false);
        devoptions.SetActive(false);

        RTSCamera = GameObject.Find("iS.RTS Camera");

       

    }
	
	// Update is called once per frame
	void Update () {
        moneyText.GetComponent<Text>();
        moneyText.text = "Gold: " + Money;

        woodText.GetComponent<Text>();
        woodText.text = "Wood: " + Wood;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseButton();
        }
    }

    public void PauseButton()
    {
        pauseMenu.SetActive(true);
        rtsCanvas = GameObject.FindGameObjectWithTag("RTSCanvas");
        rtsCanvas.SetActive(false);
        Time.timeScale = 0;

        if(PlayerController.RtsCamera == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            fpsCanvas = GameObject.FindGameObjectWithTag("FPSCanvas");
            fpsCanvas.SetActive(false);
        }
        
    }

    public void UnPauseButton()
    {
        pauseMenu.SetActive(false);
        optionsPanel.SetActive(false);
        devoptions.SetActive(false);
        rtsCanvas.SetActive(true);
        Time.timeScale = 1;

        if(PlayerController.RtsCamera == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            fpsCanvas.SetActive(true);

        }
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenOptionsPanel()
    {
       optionsPanel.SetActive(true);
    }

    public void CloseOptionsPanel()
    {
        optionsPanel.SetActive(false);
    }

    public void DevOptions()
    {
        devoptions.SetActive(true);
    }

    public void CloseDevOptions()
    {
        devoptions.SetActive(false);
    }



    public void BuildPanelOpen()
    {
        RTSCamera.GetComponent<ISRTSCamera>().MouseScrollControl(false);
        RTSCamera.GetComponent<ISRTSCamera>().enabled = false;
        BuildMenu.inMenu = true;
        buildPanel.SetActive(true);
        buildMenuOpen = true;
        
        


    }

    public void BuildPanelClose()
    {
        RTSCamera.GetComponent<ISRTSCamera>().MouseScrollControl(true);
        RTSCamera.GetComponent<ISRTSCamera>().enabled = true;
        buildPanel.SetActive(false);
        BuildMenu.ghostActive = false;
        buildMenuOpen = false;
        
        
    }

}
