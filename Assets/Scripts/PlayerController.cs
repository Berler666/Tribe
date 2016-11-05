using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject RTSCamera;
    public GameObject FPSCamera;

    public GameObject player;

    public GameObject prefab;

    public GameObject fpsCanvas;

    public float thrust;

    public static bool RtsCamera = false;

    public static bool isAttacking = false;

    public GameObject pauseMenu;


	// Use this for initialization
	void Start ()
    {
        RTSMode();
        
    }
	
	// Update is called once per frame
	void Update () {




        if (Input.GetKeyDown("p"))
        {
            if (RtsCamera == true)
            {
                FPSMode();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if(RtsCamera == false)
            {
                RTSMode();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                
            }
        }

        if (RTSCamera == false)
        {

            if (Input.GetKeyDown("x"))
            {
                Transform playerPos = player.GetComponent<Transform>();
                GameObject create;
                create = Instantiate(prefab, player.transform.position, player.transform.rotation) as GameObject;

                create.GetComponent<Rigidbody>().AddForce(playerPos.forward * thrust);
            }
        }

       if(RtsCamera == false)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
            }
        }

    }



    void RTSMode()
    {
        RTSCamera.GetComponent<Camera>().enabled = true;
        RTSCamera.GetComponent<AudioListener>().enabled = true;
        RTSCamera.GetComponent<ISRTSCamera>().enabled = true;
        FPSCamera.SetActive(false);
        RtsCamera = true;
       
        player.SetActive(false);
        fpsCanvas.SetActive(false);
    }

    void FPSMode()
    {
        RTSCamera.GetComponent<Camera>().enabled = false;
        RTSCamera.GetComponent<AudioListener>().enabled = false;
        RTSCamera.GetComponent<ISRTSCamera>().enabled = false;
        FPSCamera.SetActive(true);
        RtsCamera = false;
        
        player.SetActive(true);
        fpsCanvas.SetActive(true);
    }
}
