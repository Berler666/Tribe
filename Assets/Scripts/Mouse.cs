using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

    RaycastHit hit;

    public static ArrayList CurrentlySelectedUnits = new ArrayList(); //of gameobject

    public GUIStyle MouseDragSkin;

    public GameObject Target;

    private Vector3 mouseDownPoint;
    private static Vector3 mouseUpPoint;
    private static Vector3 currentMousePoint; //in world space

    public static bool UserIsDragging;
    private static float TimeLimitBeforeDrag = 1f;
    private static float TimeLeftBeforeDeclaringDrag;
    private static Vector2 MouseDragStart;

    private static float clickDragZone = 1.3f;

    void Awake()
    {
        mouseDownPoint = Vector3.zero;
    }

	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            currentMousePoint = hit.point;

            //store point at mouse button down
            if (Input.GetMouseButtonDown(0))
                mouseDownPoint = hit.point;

            //Mouse Drag
            if(Input.GetMouseButtonDown(0))
            {
                TimeLeftBeforeDeclaringDrag = TimeLimitBeforeDrag;
                MouseDragStart = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                //if the use is not dragging, lets do the tests
                if(!UserIsDragging)
                {
                    TimeLeftBeforeDeclaringDrag -= Time.deltaTime;
                    if(TimeLeftBeforeDeclaringDrag <= 0f || UserDraggingByPosition(MouseDragStart, Input.mousePosition))
                    {
                        //if tests pass (true) user is dragging!
                        UserIsDragging = true;
                    }

                }

                //ok user is dragging lets compete (GUI...)
                if(UserIsDragging)
                Debug.Log("yes the user is dragging");
            }
            else if(Input.GetMouseButtonUp(0))
            {
                if(UserIsDragging)
                    Debug.Log("user is not dragging");

                TimeLeftBeforeDeclaringDrag = 0f;
                UserIsDragging = false;
            }

            

            //Mouse click
            if (!UserIsDragging)
            {

                if (hit.collider.name == "TerrainMain")
                {

                    //When we click the right mouse button, instaniate target
                    if (Input.GetMouseButtonDown(1))
                    {
                        GameObject TargetObj = Instantiate(Target, hit.point, Quaternion.identity) as GameObject;
                        TargetObj.name = "Taget Instantiated";
                    }
                    else if (Input.GetMouseButtonUp(0) && DidUserClickLeftMouse(mouseDownPoint))
                        if (!ShiftKeysDown())
                            DeselectGameObjectsIfSelected();

                }  // end of the terrain

                else {

                    //hitting other objects
                    if (Input.GetMouseButtonUp(0) && DidUserClickLeftMouse(mouseDownPoint))
                    {

                        //is the user hitting a unit?
                        if (hit.collider.transform.FindChild("Selected"))
                        {

                            //found a unit we can select
                            Debug.Log("found a unit");

                            //are we selecting a different object?
                            if (!UnitAlreadyInCurrentlySelectedUnits(hit.collider.gameObject))
                            {

                                //if the shift key is not down remove the rest of the units
                                if (!ShiftKeysDown())
                                    DeselectGameObjectsIfSelected();

                                GameObject SelectedObj = hit.collider.transform.FindChild("Selected").gameObject;
                                SelectedObj.SetActive(true);

                                //add unit to currently selected units
                                CurrentlySelectedUnits.Add(hit.collider.gameObject);

                            }
                            else {
                                //unit is already in the currently selected units arraylist
                                //remove the unit!
                                if (ShiftKeysDown())
                                    RemoveUnitFromCurrentlySelectedUnits(hit.collider.gameObject);
                                else
                                {
                                    DeselectGameObjectsIfSelected();
                                    GameObject SelectedObj = hit.collider.transform.FindChild("Selected").gameObject;
                                    SelectedObj.SetActive(true);
                                    CurrentlySelectedUnits.Add(hit.collider.gameObject);
                                }
                            }

                        }
                        else {

                            //if this object is not a unit
                            if (!ShiftKeysDown())
                                DeselectGameObjectsIfSelected();
                        }
                    }
                }

            }
            else {
                if (Input.GetMouseButtonUp(0) && DidUserClickLeftMouse(mouseDownPoint))
                {
                    if (!ShiftKeysDown())
                        DeselectGameObjectsIfSelected();
                }
            }// end of raycasthits
        } // end of is dragging?

        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow);
    } 
       
    void OnGUI()
    {
        //box width, height, top, left
        if (UserIsDragging)
        {
            float BoxWidth = Camera.main.WorldToScreenPoint(mouseDownPoint).x - Camera.main.WorldToScreenPoint(currentMousePoint).x;
            float BoxHeight = Camera.main.WorldToScreenPoint(mouseDownPoint).y - Camera.main.WorldToScreenPoint(currentMousePoint).y;


            float BoxLeft, BoxTop;
            BoxLeft = Input.mousePosition.x;
            BoxTop = (Screen.height - Input.mousePosition.y) - BoxHeight;
            GUI.Box(new Rect(BoxLeft,
                BoxTop,
                BoxWidth,
                BoxHeight), "", MouseDragSkin);
        }

    }

    #region Helper functions
    
    //is the user dragging relative to the mouse drag start point
    public bool UserDraggingByPosition(Vector2 DragStartPoint, Vector2 NewPoint)
    {
        if (
            (NewPoint.x > DragStartPoint.x + clickDragZone || NewPoint.x < DragStartPoint.x - clickDragZone) ||
            (NewPoint.y > DragStartPoint.y + clickDragZone || NewPoint.y < DragStartPoint.y - clickDragZone)
            )
            return true;
        else return false;
    }

     //check if user clicked mouse
    public bool DidUserClickLeftMouse(Vector3 hitpoint)
    {
        
        if (
            (mouseDownPoint.x < hitpoint.x + clickDragZone && mouseDownPoint.x > hitpoint.x - clickDragZone) &&
            (mouseDownPoint.y < hitpoint.y + clickDragZone && mouseDownPoint.y > hitpoint.y - clickDragZone) &&
            (mouseDownPoint.z < hitpoint.z + clickDragZone && mouseDownPoint.z > hitpoint.z - clickDragZone)
            )
            return true;
        else return false;
    }

    //deselects gameobject if selected
    public static void DeselectGameObjectsIfSelected()
    {
        if(CurrentlySelectedUnits.Count > 0)
        {
            for(int i = 0; i < CurrentlySelectedUnits.Count; i++)
            {
                GameObject ArrayListUnit = CurrentlySelectedUnits[i] as GameObject;
                ArrayListUnit.transform.FindChild("Selected").gameObject.SetActive(false);
                
            }

            CurrentlySelectedUnits.Clear();
        }
    }

    //check if a unit is already in the currently select units arraylist
    public static bool UnitAlreadyInCurrentlySelectedUnits(GameObject Unit)
    {
        if(CurrentlySelectedUnits.Count > 0)
        {
            for(int i = 0; i < CurrentlySelectedUnits.Count; i++)
            {
                GameObject ArrayListUnit = CurrentlySelectedUnits[i] as GameObject;
                if (ArrayListUnit == Unit)
                    return true;
            }

            return false;
        } else
        {
            return false;
        }
    }

    //remove a unit from the currently selected units arraylist
    public void RemoveUnitFromCurrentlySelectedUnits(GameObject Unit)
    {
        if (CurrentlySelectedUnits.Count > 0)
        {
            for (int i = 0; i < CurrentlySelectedUnits.Count; i++)
            {
                GameObject ArrayListUnit = CurrentlySelectedUnits[i] as GameObject;
                if (ArrayListUnit == Unit)
                {
                    CurrentlySelectedUnits.RemoveAt(i);
                    ArrayListUnit.transform.FindChild("Selected").gameObject.SetActive(false);
                }     
            }

            return;
        }
        else
        {
            return;
        }
    }

    //are the shift keys being held down?
    public static bool ShiftKeysDown()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            return true;
        else return false;
        

        
    }

    #endregion
}
