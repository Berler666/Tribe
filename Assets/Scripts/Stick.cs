using UnityEngine;
using System.Collections;

public class Stick : MonoBehaviour {

    public GameObject stickUI;
    Transform player;

    bool uiActive = false;

    PlayerInventory playerinv;

	// Use this for initialization
	void Start () {

        stickUI.SetActive(false);
        stickUI.GetComponent<Transform>();
        playerinv = GameObject.Find("Player Controller").GetComponent<PlayerInventory>();
	}
	
	// Update is called once per frame
	void Update () {

        


        if (uiActive == true)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            Vector3 targetPostition = new Vector3(player.position.x, stickUI.transform.position.y, player.position.z);
            stickUI.transform.LookAt(targetPostition);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (playerinv.canCollectWood == false)
                {
                    Debug.Log("inv full");

                }

                if (playerinv.canCollectWood == true)
                {
                    uiActive = false;
                    Collect();
                }

            }
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            stickUI.SetActive(true);
            uiActive = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            stickUI.SetActive(false);
            uiActive = false;
        }
    }

    void Collect()
    {
        playerinv.pWood += 1;
        Debug.Log("+1 wood");
        Destroy(gameObject);

    }
}
