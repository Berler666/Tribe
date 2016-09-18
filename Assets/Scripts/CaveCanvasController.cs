using UnityEngine;
using System.Collections;

public class CaveCanvasController : MonoBehaviour {

    public GameObject workBenchUI;
    public GameObject caveUI;
    public static bool caveCanvas = false;
    public GameObject fpsHud;
    public GameObject player;
    public GameObject CaveUICamera;

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
