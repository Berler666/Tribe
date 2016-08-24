using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

    #region Class Variables

    RaycastHit hit;

    public static Vector3 RightClickPoint;
    public static ArrayList CurrentlySelectedUnits = new ArrayList(); //of gameobject
    public static ArrayList UnitsOnScreen = new ArrayList(); //of gameobject
    public static ArrayList UnitsInDrag = new ArrayList(); // of gameobject
    private bool FinishedDragOnThisFrame;

    public GUIStyle MouseDragSkin;

    public GameObject Target;

    private Vector3 mouseDownPoint;
    private static Vector3 currentMousePoint; //in world space

    public static bool UserIsDragging;
    private static float TimeLimitBeforeDrag = 1f;
    private static float TimeLeftBeforeDeclaringDrag;
    private static Vector2 MouseDragStart;

    private static float clickDragZone = 1.3f;

    //GUI
    private float boxWidth;
    private float boxHeight;
    private float boxTop;
    private float boxLeft;
    private static Vector2 boxStart;
    private static Vector2 boxFinish;

    #endregion

    void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            currentMousePoint = hit.point;

            //store point at mouse button down
      
            if(Input.GetMouseButtonDown(0))
            {
                mouseDownPoint = hit.point;
                TimeLeftBeforeDeclaringDrag = TimeLimitBeforeDrag;
                MouseDragStart = Input.mousePosition;

                if (!ShiftKeysDown())
                {
                    DeselectGameObjectsIfSelected();
                }
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
            }
            else if(Input.GetMouseButtonUp(0))
            {
                if (UserIsDragging)
                    FinishedDragOnThisFrame = true;

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
                        //GameObject TargetObj = Instantiate(Target, hit.point, Quaternion.identity) as GameObject;
                        //TargetObj.name = "Taget Instantiated";
                        RightClickPoint = hit.point;
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
                        if (hit.collider.gameObject.GetComponent<Unit>())
                        {

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

                                //change the unit selected value to true;
                                hit.collider.gameObject.GetComponent<Unit>().Selected = true;

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

        if (UserIsDragging)
        {

            //GUI vars
            boxWidth = Camera.main.WorldToScreenPoint(mouseDownPoint).x - Camera.main.WorldToScreenPoint(currentMousePoint).x;
            boxHeight = Camera.main.WorldToScreenPoint(mouseDownPoint).y - Camera.main.WorldToScreenPoint(currentMousePoint).y;

            boxLeft = Input.mousePosition.x;
            boxTop = (Screen.height - Input.mousePosition.y) - boxHeight;

            if(Common.FloatToBool(boxWidth))
            {
                if(Common.FloatToBool(boxHeight))
                {
                    boxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y + boxHeight);
                }
                else
                {
                    boxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                }
            }
            else
            {
                if(!Common.FloatToBool(boxWidth))
                {
                    if(Common.FloatToBool(boxHeight))
                    {
                        boxStart = new Vector2(Input.mousePosition.x + boxWidth, Input.mousePosition.y + boxHeight);
                    }
                    else
                    {
                        boxStart = new Vector2(Input.mousePosition.x + boxWidth, Input.mousePosition.y);
                    }
                }
            }

            boxFinish = new Vector2(
           boxStart.x + Common.Unsigned(boxWidth),
           boxStart.y - Common.Unsigned(boxHeight)
       );

           // Debug.Log(boxStart.x + "," + boxFinish.x);
        }
    } 

    void LateUpdate()
    {
        UnitsInDrag.Clear();

        //if user is dragging or finished on this frame AND there are units to select on the screen
        if ((UserIsDragging || FinishedDragOnThisFrame) && UnitsOnScreen.Count > 0)
        {
            for(int i = 0; i < UnitsOnScreen.Count; i++)
            {
                GameObject UnitObj = UnitsOnScreen[i] as GameObject;
                Unit UnitScript = UnitObj.GetComponent<Unit>();
                GameObject SelectedObj = UnitObj.transform.FindChild("Selected").gameObject;

                //if not already in the drag units
                if(!UnitAlreadyInDraggedUnits(UnitObj))
                {
                    if(UnitIsInsideDrag(UnitScript.ScreenPos))
                    {
                        SelectedObj.SetActive(true);
                        UnitsInDrag.Add(UnitObj);
                    }
                    else
                    {
                        //remove the selected graphic if unit is not already in currently selcted units
                        if (!UnitAlreadyInCurrentlySelectedUnits(UnitObj))
                            SelectedObj.SetActive(false);
                    }
                }
            }
        }


        if(FinishedDragOnThisFrame)
        {
            FinishedDragOnThisFrame = false;
            PutDraggedUnitsInCurrentlySelectedUnits();
        }


    }
       
    void OnGUI()
    {
        //box width, height, top, left
        if (UserIsDragging)
        {
           
            GUI.Box(new Rect(boxLeft,
                boxTop,
                boxWidth,
                boxHeight), "", MouseDragSkin);
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
                ArrayListUnit.GetComponent<Unit>().Selected = false;
                
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


    //check if a unit is withinnthe screen space to deal with mouse drag selecting
    public static bool UnitWithinScreenSpace(Vector2 UnitScreenPos)
    {
        if (
            (UnitScreenPos.x < Screen.width && UnitScreenPos.y < Screen.height) &&
            (UnitScreenPos.x > 0f && UnitScreenPos.y > 0f)
            )
            return true;
        else return false;
    }


    //Remove unit from units UnitsOnScreen arraylist
    public static void RemoveFromOnScreenUnits(GameObject Unit)
    {
        for(int i = 0; i < UnitsOnScreen.Count; i++)
        {
            GameObject UnitObj = UnitsOnScreen[i] as GameObject;
            if(Unit == UnitObj)
            {
                UnitsOnScreen.RemoveAt(i);
                UnitObj.GetComponent<Unit>().OnScreen = false;
                return;
            }
        }
        return;
    }

    //is unit inside the drag
    public static bool UnitIsInsideDrag(Vector2 UnitScreenPos)
    {
        if (
            (UnitScreenPos.x > boxStart.x && UnitScreenPos.y < boxStart.y) &&
            (UnitScreenPos.x < boxFinish.x && UnitScreenPos.y > boxFinish.y)
            ) return true;
        else return false;
    }



    //check if unit is in UnitsInDrag array lis
    public static bool UnitAlreadyInDraggedUnits(GameObject Unit)
    {
        if (UnitsInDrag.Count > 0)
        {
            for (int i = 0; i < CurrentlySelectedUnits.Count; i++)
            {
                GameObject ArrayListUnit = UnitsInDrag[i] as GameObject;
                if (ArrayListUnit == Unit)
                    return true;
            }

            return false;
        }
        else
        {
            return false;
        }
    }


    //take all units from unitsInDrag into currentlyselectedUNits
    public static void PutDraggedUnitsInCurrentlySelectedUnits()
    {
       

        if(UnitsInDrag.Count > 0)
        {
            for(int i = 0; i < UnitsInDrag.Count; i++)
            {
                GameObject UnitObj = UnitsInDrag[i] as GameObject;

                //if unit is not already in currentlySelectedUnits, add it
                if(!UnitAlreadyInCurrentlySelectedUnits(UnitObj))
                {
                    CurrentlySelectedUnits.Add(UnitObj);
                    UnitObj.GetComponent<Unit>().Selected = true;
                }
            }

            UnitsInDrag.Clear();
        }
    }

    #endregion
}
