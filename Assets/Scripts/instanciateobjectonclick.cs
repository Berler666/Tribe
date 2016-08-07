using UnityEngine;

using System.Collections;

public class instanciateobjectonclick : MonoBehaviour
{

    Ray ray;
    RaycastHit hit;
    public GameObject Camera1;

    public bool placed = false;

    // Buildings //

    //hut
    public GameObject Hut;
    public static bool hut = false;
    public int hutPrice;

    //gathering hut
    public GameObject GatheringHut;
    public static bool gatheringhut = false;
    public int ghutPrice;

    //chief hut
    public GameObject Chiefhut;
    public static bool chiefhut = false;
    public int chutPrice;


    // Use this for initialization
    void Start()
    {

       

    }

    // Update is called once per frame
    void Update()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Hut //
        if (hut == true)
        {
            if (GameUIController.Money >= hutPrice)
            {
                if (placed == false)
                {

                    if (Physics.Raycast(ray, out hit))
                    {

                        if (Input.GetKey(KeyCode.Mouse0))
                        {
                            Camera1.GetComponent<RTSCamera>().enabled = false;
                            GameObject obj = Instantiate(Hut, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.Euler(-90, Random.Range(0, 360), 0)) as GameObject;
                            placed = true;

                            StartCoroutine(WaitToPlace());
                            StartCoroutine(TurnCameraMovementOn());
                            GameUIController.Money -= hutPrice;
                            if (PopulationController.populationCountLmit <= PopulationController.populationCountMaxLmit -1)
                            { 
                                PopulationController.populationCountLmit += 5;
                            }
                            hut = false;
                        }

                    }
                }
            }
        }

        // Gathering hut //
        if (gatheringhut == true)
        {
            if (GameUIController.Money >= ghutPrice)
            {
                if (placed == false)
                {

                    if (Physics.Raycast(ray, out hit))
                    {

                        if (Input.GetKey(KeyCode.Mouse0))
                        {
                            Camera1.GetComponent<RTSCamera>().enabled = false;
                            GameObject obj = Instantiate(GatheringHut, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity) as GameObject;
                            placed = true;

                            StartCoroutine(WaitToPlace());
                            StartCoroutine(TurnCameraMovementOn());
                            GameUIController.Money -= ghutPrice;
                            gatheringhut = false;
                        }

                    }
                }
            }
        }

        // Chief hut //
        if (chiefhut == true)
        {
            if (GameUIController.Money >= chutPrice)
            {
                if (placed == false)
                {

                    if (Physics.Raycast(ray, out hit))
                    {

                        if (Input.GetKey(KeyCode.Mouse0))
                        {
                            Camera1.GetComponent<RTSCamera>().enabled = false;
                            GameObject obj = Instantiate(Chiefhut, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity) as GameObject;
                            placed = true;

                            StartCoroutine(WaitToPlace());
                            StartCoroutine(TurnCameraMovementOn());
                            GameUIController.Money -= chutPrice;
                            GameUIController.Wood -= 100;
                            chiefhut = false;
                        }

                    }
                }
            }
        }

    }

    IEnumerator WaitToPlace()
    {
        yield return new WaitForSeconds(1f);
        placed = false;
    }

    IEnumerator TurnCameraMovementOn()
    {
        yield return new WaitForSeconds(0.2f);
        Camera1.GetComponent<RTSCamera>().enabled = true;
    }



}