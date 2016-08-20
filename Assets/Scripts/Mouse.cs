using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

    RaycastHit hit;

    public static GameObject CurrentlySelectedUnit;

    public GameObject Target;

    private Vector3 mouseDownPoint;

    void Awake()
    {
        mouseDownPoint = Vector3.zero;
    }

	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //store point at mouse button down
            if(Input.GetMouseButtonDown(0))
                mouseDownPoint = hit.point;

            if (hit.collider.name == "TerrainMain")
            {

                //When we click the right mouse button, instaniate target
                if (Input.GetMouseButtonDown(1))
                {
                    GameObject TargetObj = Instantiate(Target, hit.point, Quaternion.identity) as GameObject;
                    TargetObj.name = "Taget Instantiated";
                }
                else if (Input.GetMouseButtonUp(0) && DidUserClickLeftMouse(mouseDownPoint))
                    DeselectGameObjectIfSelected();

            }  // end of the terrain

            else {

                //hitting other objects
                if (Input.GetMouseButtonUp(0) && DidUserClickLeftMouse(mouseDownPoint))
                {

                    //is the user hitting a unt?
                    if (hit.collider.transform.FindChild("Selected"))
                    {

                        //found a unit we can select
                        Debug.Log("found a unit");

                        //are we selecting a different object?
                        if (CurrentlySelectedUnit != hit.collider.gameObject)
                        {
                            //Activate the selector
                            GameObject SelectedObj = hit.collider.transform.FindChild("Selected").gameObject;
                            SelectedObj.SetActive(true);

                            //deactivate the currently selected objects selector
                            if (CurrentlySelectedUnit != null)
                                CurrentlySelectedUnit.transform.FindChild("Selected").gameObject.SetActive(false);

                            //replace cyrrently selected unit
                            CurrentlySelectedUnit = hit.collider.gameObject;
                        }

                    } else {

                        //if this object is not a unit
                        DeselectGameObjectIfSelected();
                    }
                }
            }
                
            } else
            {
                if(Input.GetMouseButtonUp(0))
                {
                    DeselectGameObjectIfSelected();
                }
            }
        Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.yellow);
    }
       
    

    #region Helper functions
    
     //check if user clicked mouse

    public bool DidUserClickLeftMouse(Vector3 hitpoint)
    {
        float clickZone = 0.8f;
        if (
            (mouseDownPoint.x < hitpoint.x + clickZone && mouseDownPoint.x > hitpoint.x - clickZone) &&
            (mouseDownPoint.y < hitpoint.y + clickZone && mouseDownPoint.y > hitpoint.y - clickZone) &&
            (mouseDownPoint.z < hitpoint.z + clickZone && mouseDownPoint.z > hitpoint.z - clickZone)
            )
            return true;
        else return false;
    }

    //deselects gameobject if selected
    public static void DeselectGameObjectIfSelected()
    {
        if(CurrentlySelectedUnit != null)
        {
            CurrentlySelectedUnit.transform.FindChild("Selected").gameObject.SetActive(false);
            CurrentlySelectedUnit = null;
        }
    }

    #endregion
}
