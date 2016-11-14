using UnityEngine;
using System.Collections;

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


    // Use this for initialization
    void Start () {
	
	}

    void Update()
    {
        if (pFood >= carryLimit)
        {
            canCollectFood = false;

        }

        if (pWood >= carryLimit)
        {
            canCollectWood = false;

        }

        if (pStone == carryLimit)
        {
            canCollectStone = false;

        }

        if (pGold >= carryLimit)
        {
            canCollectGold = false;

        }

    }

}

