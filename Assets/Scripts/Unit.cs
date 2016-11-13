using UnityEngine;
using System.Collections;

/*
 * this script should be attached to all controllable units in the game, wether they move or not
 */

public class Unit : MonoBehaviour {

    //for mouse.cs
    public Vector2 ScreenPos;
    public bool OnScreen;
    public bool Selected = false;

    public bool isWalkable = true;
    public bool canCollectResourses = false;

    public int resource = 0;
    public int resourcesPerGather = 2;
    public int maxResourceCarry = 10;
    public float waitTime = 5;
    private bool isGathering;

    TreeController treeScript;



    void Start()
    {
        //adds this unit to the population count
        PopulationController.populationCount += 1;
    }

    void Update()
    {
        //if the unit is not selected get screen space
        if(!Selected)
        {
            //track the screen position
            ScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);

            //if within the screen space
            if(Mouse.UnitWithinScreenSpace(ScreenPos))
            {
                //and not already added to unitsOnScreen, add it
                if(!OnScreen)
                {
                    Mouse.UnitsOnScreen.Add(this.gameObject);
                    OnScreen = true;
                }
            }
            else
            {
                //remove if previously on screen
                if(OnScreen)
                {
                    Mouse.RemoveFromOnScreenUnits(this.gameObject);
                }
            }
        }

        //Stop gathering when carry limit is reached
        if(resource >= maxResourceCarry)
        {
            Debug.Log("carry limit reached");
            isGathering = false;
        }

    }

    void OnTriggerEnter(Collider resourceCol)
    {
        if (resourceCol.tag == "resource")
        {
            treeScript = resourceCol.GetComponent<TreeController>();
            Debug.Log("gathering resource");
            isGathering = true;
            StartCoroutine(OnGatherUpdate());
        }

        if (resourceCol.tag == "stockpile")
        {

            Debug.Log("depositing resources");
            StartCoroutine(OnDepositeUpdate());
            
        }
    }

    void OnTriggerExit(Collider resourceCol)
    {
        if (resourceCol.tag == "resource")
        {
            
            Debug.Log("stop gathering resource");
            isGathering = false;
            StopCoroutine(OnGatherUpdate());
        }

        if (resourceCol.tag == "stockpile")
        {

            Debug.Log("stop deposite");
            
            
        }
    }


    IEnumerator OnGatherUpdate()
    {
        yield return new WaitForSeconds(waitTime);
        if (isGathering == true)
        {
            Debug.Log("+ 2 wood");
            resource += resourcesPerGather;
            treeScript.treeWood -= resourcesPerGather;
            StartCoroutine(OnGatherUpdate());
        }
    }

    IEnumerator OnDepositeUpdate()
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("deposite complete");
        PlayerResources.wood += resource;
        resource = 0;
    }
}
