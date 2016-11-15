using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    public int pFood;
    public int pWood;
    public int pStone;
    public int pGold;

    public int carryLimit = 15;

    public bool canCollectWood = true;
    public bool canCollectFood = true;
    public bool canCollectStone = true;
    public bool canCollectGold = false;


    public GameObject playerInv;

    bool invIsOpen = false;

    public Text foodTxt;
    public Text woodTxt;
    public Text stoneTxt;
    public Text GoldTxt;

    // Use this for initialization
    void Start () {

        foodTxt.GetComponent<Text>();
        woodTxt.GetComponent<Text>();
        stoneTxt.GetComponent<Text>();
        GoldTxt.GetComponent<Text>();

        playerInv.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(invIsOpen == false)
            {
                playerInv.SetActive(true);
                invIsOpen = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                foodTxt.text = "Food " + pFood;
                woodTxt.text = "Wood " + pWood;
                stoneTxt.text = "Stone " + pStone;
                GoldTxt.text = "Gold " + pGold;
            }
            else
            {
                playerInv.SetActive(false);
                invIsOpen = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
                
        }

        if (pFood >= carryLimit)
        {
            canCollectFood = false;

        }

        if (pWood >= carryLimit)
        {
            canCollectWood = false;

        }

        if (pStone >= carryLimit)
        {
            canCollectStone = false;

        }

        if (pGold >= carryLimit)
        {
            canCollectGold = false;

        }

    }

    public void CloseInv()
    {
        playerInv.SetActive(false);
        invIsOpen = false;
    }

}

