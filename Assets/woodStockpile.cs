using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class woodStockpile : MonoBehaviour {

   public GameObject stockPileUI;
    bool uiActive = false;

    Transform player;

    public Text text;

    PlayerInventory playerinv;

    void Start () {
        stockPileUI.SetActive(false);
        stockPileUI.GetComponent<Transform>();
        playerinv = GameObject.Find("Player Controller").GetComponent<PlayerInventory>();

    }
	
	// Update is called once per frame
	void Update () {


        if (uiActive == true)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            Vector3 targetPostition = new Vector3(player.position.x, stockPileUI.transform.position.y, player.position.z);
            stockPileUI.transform.LookAt(targetPostition);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Deposit();

            }
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            stockPileUI.SetActive(true);
            uiActive = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            stockPileUI.SetActive(false);
            
        }
    }

    void Deposit()
    {
        playerinv.pWood += PlayerResources.wood;
        text.text = "Plus " + playerinv.pWood + " Wood";
        playerinv.pWood -= playerinv.pWood;
        StartCoroutine(HideText());
    }

    IEnumerator HideText()
    {
        yield return new WaitForSeconds(4);
        text.text = "  ";
    }
}
