using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject RTSCamera;
    public GameObject FPSCamera;

    public GameObject player;

    public GameObject prefab;

    public GameObject fpsCanvas;

    public float thrust;

    bool RtsCamera = true;

    public static bool isAttacking = false;


	// Use this for initialization
	void Start ()
    {
        RTSCamera.SetActive(true);
        FPSCamera.SetActive(false);
        RtsCamera = true;
        Cursor.visible = true;
        fpsCanvas.SetActive(false);
        player.SetActive(false);
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("q"))
        {
            isAttacking = true;
        }

        if (Input.GetKeyDown("p"))
        {
            if (RtsCamera == true)
            {
                RTSCamera.SetActive(false);
                FPSCamera.SetActive(true);
                RtsCamera = false;
                Cursor.visible = false;
                player.SetActive(true);
                fpsCanvas.SetActive(true);
            }
            else if(RtsCamera == false)
            {
                RTSCamera.SetActive(true);
                FPSCamera.SetActive(false);
                RtsCamera = true;
                Cursor.visible = true;
                player.SetActive(false);
                fpsCanvas.SetActive(false);
            }
        }

        if (RTSCamera == false)
        {

            if (Input.GetKeyDown("e"))
            {
                Transform playerPos = player.GetComponent<Transform>();
                GameObject create;
                create = Instantiate(prefab, player.transform.position, player.transform.rotation) as GameObject;

                create.GetComponent<Rigidbody>().AddForce(playerPos.forward * thrust);
            }
        }

       

    }
}
