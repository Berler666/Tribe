using UnityEngine;
using System.Collections;
using Pathfinding;

public class BuildMenu : MonoBehaviour {

    public GameObject ghost;
    public GameObject Building;
    public GameObject campfireGhostPrefab;
    public Material GreenTransparent;
    public Material RedTransparent;
    public static bool ghostActive = false;

    public GameObject campFirePrefab;
    public int campfireResources = 10; 

    public int resources;

    public static bool canBuildUnit;

    public static bool PassedTriggerTest;
    public static bool PassedHightsTest;

    public static bool inMenu = false;

    public static BuildMenu Instance;

    public static Vector3 GroundMousePoint;
    public LayerMask GroundOnly;
    RaycastHit hit;

    void Start()
    {
        Instance = this;
    }

	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, GroundOnly))
        {
            GroundMousePoint = hit.point;

        }

     }

   

    void LateUpdate()
    {
        if(ghostActive)
        {
            if(PassedHightsTest && PassedTriggerTest)
            {
                ghost.GetComponent<Renderer>().material = GreenTransparent;
                canBuildUnit = true;
            }
            else
            {
                ghost.GetComponent<Renderer>().material = RedTransparent;
                canBuildUnit = false;
            }

            if(Input.GetMouseButtonUp(0) && canBuildUnit && !inMenu)
            {
                Debug.Log("Building the unit");
                GameObject newUnit = Instantiate(Building, GroundMousePoint, Quaternion.identity) as GameObject;
                newUnit.transform.eulerAngles = ghost.transform.eulerAngles;
                ghostActive = false;
                Destroy(ghost);

                if(ghost.transform.FindChild("HightPoints"))
                {
                    newUnit.transform.position = new Vector3(newUnit.transform.position.x, ghost.GetComponent<HightPoints>().tallestHight, newUnit.transform.position.z);
                    
                }
               AstarPath.active.UpdateGraphs(newUnit.GetComponent<BoxCollider>().bounds);

            }

            if (Input.GetMouseButtonUp(0) && !canBuildUnit && inMenu)
            {
                Debug.Log("Cannot build");
            }
        }

        inMenu = false;
    }

    public void buildObject1()
    {
        if (ghostActive)
        {
            Destroy(ghost);
        }
        ghost = Instantiate(campfireGhostPrefab, Vector3.zero, Quaternion.Euler(-90, 0, 0)) as GameObject;
        Building = campFirePrefab;
        ghostActive = true;
        inMenu = true;

    }
}
