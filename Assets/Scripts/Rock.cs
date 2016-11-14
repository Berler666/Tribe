using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

    public GameObject rockUI;
    Transform player;

    bool uiActive = false;

    PlayerInventory playerinv;

    
    // Use this for initialization
    void Start()
    {

        rockUI.SetActive(false);
        rockUI.GetComponent<Transform>();
        playerinv = GameObject.Find("Player Controller").GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {



        if (uiActive == true)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            Vector3 targetPostition = new Vector3(player.position.x, rockUI.transform.position.y, player.position.z);
            rockUI.transform.LookAt(targetPostition);


            if (Input.GetKeyDown(KeyCode.E))
            {
                if(playerinv.canCollectStone == false)
                {
                    Debug.Log("inv full");
                   
                } 

                if(playerinv.canCollectStone == true)
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
            rockUI.SetActive(true);
            uiActive = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            rockUI.SetActive(false);
            uiActive = false;
        }
    }

    void Collect()
    {
        playerinv.pStone += 1;
        Debug.Log("+1 stone");
        Destroy(gameObject);

    }
}
