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
    }



}
