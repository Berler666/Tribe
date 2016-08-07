using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChiefHutController : MonoBehaviour {

    public GameObject ChiefHutpanel;
    public GameObject Villager;
 

    bool isover;

	// Use this for initialization
	void Start ()
    {
        ChiefHutpanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))
        {
            if (isover == true)
            {
                ChiefHutpanel.SetActive(true);
                Debug.Log("bruh");
            }
        }

    }

    void OnMouseEnter()
    {
        isover = true;
    }

    void OnMouseExit()
    {
        isover = false;
    }

    public void CloseCheifHutMenu()
    {
        ChiefHutpanel.SetActive(false);
    }

    public void CreateVillager()
    {
        PopulationController.populationCount += 1;

        GameObject villager = Instantiate(Villager, new Vector3(transform.position.x+5, transform.position.y-1, transform.position.z+5), Quaternion.identity) as GameObject;
    }
}
