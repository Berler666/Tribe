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

    public float ResearchWaitTime = 10f;

    //Resourse text
    public Text PopulationTxt;
    public Text ReserchpointsTxt;
    public Text FoodTxt;
    public Text WoodTxt;
    public Text StoneTxt;
    public Text GoldTxt;

    //Research Buttons
    public Button researchButton1;
    public Button researchButton2;
    public Button researchButton3;
    public Button researchButton4;
    public Button researchButton5;
    public Button researchButton6;
    public Button researchButton7;

    //Research item bools
    public static bool sharpenedStick = false;
    public static bool sharpenedRock = false;
    public static bool fire = false;
    public static bool gatheringBench = false;
    public static bool huntingBench = false;
    public static bool furBed = false;
    public static bool furCloths = false;


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

        if(huntingBench == false)
        {
            researchButton6.interactable = false;
            researchButton7.interactable = false;
        }
       
    }

    public void Migrate()
    {
        UnitPathFinding[] units = FindObjectsOfType(typeof(UnitPathFinding)) as UnitPathFinding[];

        foreach(UnitPathFinding unit in units)
        {
            unit.enabled = true;
        }
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

    public void Research1()
    {
        if(PlayerResources.ReserchPoints > 50)
        {
            PlayerResources.ReserchPoints -= 50;
            sharpenedStick = true;
            researchButton1.interactable = false;
        }
        else
        {
            Debug.Log("not enough RP");
        }
    }

    public void Research2()
    {
        if (PlayerResources.ReserchPoints > 50)
        {
            PlayerResources.ReserchPoints -= 50;
            sharpenedRock = true;
            researchButton2.interactable = false;
        }
        else
        {
            Debug.Log("not enough RP");
        }
    }

    public void Research3()
    {
        if (PlayerResources.ReserchPoints > 200)
        {
            PlayerResources.ReserchPoints -= 200;
            fire = true;
            researchButton3.interactable = false;
        }
        else
        {
            Debug.Log("not enough RP");
        }
    }

    public void Research4()
    {
        if (PlayerResources.ReserchPoints > 150)
        {
            PlayerResources.ReserchPoints -= 150;
            gatheringBench = true;
            researchButton4.interactable = false;
        }
        else
        {
            Debug.Log("not enough RP");
        }
    }

    public void Research5()
    {
        if (PlayerResources.ReserchPoints > 200)
        {
            PlayerResources.ReserchPoints -= 200;
            huntingBench = true;
            researchButton5.interactable = false;
            researchButton6.interactable = true;
            researchButton7.interactable = true;

        }
        else
        {
            Debug.Log("not enough RP");
        }
    }

    public void Research6()
    {
        if (PlayerResources.ReserchPoints > 100)
        {
            PlayerResources.ReserchPoints -= 100;
            furBed = true;
            researchButton6.interactable = false;
        }
        else
        {
            Debug.Log("not enough RP");
        }
    }

    public void Research7()
    {
        if (PlayerResources.ReserchPoints > 125)
        {
            PlayerResources.ReserchPoints -= 125;
            furCloths = true;
            researchButton7.interactable = false;
        }
        else
        {
            Debug.Log("not enough RP");
        }
    }
}
