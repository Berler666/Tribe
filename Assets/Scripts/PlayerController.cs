using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject RTSCamera;
    public GameObject FPSCamera;

    public Transform player;

    public GameObject prefab;

    public float thrust;

    bool RtsCamera = true;

    public static bool isAttacking = false;


	// Use this for initialization
	void Start ()
    {
        RTSCamera.SetActive(true);
        FPSCamera.SetActive(false);
        RtsCamera = true;
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
            }
            else if(RtsCamera == false)
            {
                RTSCamera.SetActive(true);
                FPSCamera.SetActive(false);
                RtsCamera = true;
            }
        }

        if (FPSCamera == true)
        {

            if (Input.GetKeyDown("e"))
            {
                GameObject create;
                create = Instantiate(prefab, player.transform.position, player.transform.rotation) as GameObject;

                create.GetComponent<Rigidbody>().AddForce(player.forward * thrust);
            }
        }

       

    }
}
