using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CaveCanvasController : MonoBehaviour {

    public GameObject workBenchUI;
    public GameObject caveUI;
    public static bool caveCanvas = false;
    public GameObject fpsHud;
    public GameObject player;
    public GameObject CaveUICamera;

    public Text PopulationTxt;
    public Text ReserchpointsTxt;
    public Text FoodTxt;
    public Text WoodTxt;
    public Text StoneTxt;
    public Text GoldTxt;

    void Start()
    {
        
    }

    void Update()
    {
       
       if(caveCanvas == true)
        {
            if(Input.GetKey(KeyCode.E))
            {
                workBenchUI.SetActive(false);
                fpsHud.SetActive(false);
                caveUI.SetActive(true);
                player.SetActive(false);
                CaveUICamera.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }
        }

       //StatsText

        PopulationTxt.GetComponent<Text>().text = "Population: " + PopulationController.populationCount + "|" + PopulationController.populationCountLmit;
        ReserchpointsTxt.GetComponent<Text>().text = "Reserch Points: " + PlayerResources.ReserchPoints;
        FoodTxt.GetComponent<Text>().text = "Food: " + PlayerResources.food;
        WoodTxt.GetComponent<Text>().text = "Wood: " + PlayerResources.wood;
        StoneTxt.GetComponent<Text>().text = "Stone: " + PlayerResources.stone;
        GoldTxt.GetComponent<Text>().text = "Gold: " + PlayerResources.gold;
    }

    public void ExitCaveMenu()
    {
        workBenchUI.SetActive(true);
        fpsHud.SetActive(true);
        caveUI.SetActive(false);
        player.SetActive(true);
        CaveUICamera.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

  

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            caveCanvas = true;
            workBenchUI.gameObject.SetActive(true);
            
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            caveCanvas = false;
            workBenchUI.gameObject.SetActive(false);
            caveUI.SetActive(false);
           
        }
    }
}
