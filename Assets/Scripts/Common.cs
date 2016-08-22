using UnityEngine;
using System.Collections;

public class Common : MonoBehaviour {

    //float to bool
    public static bool FloatToBool(float Float)
    {
        if (Float < 0f) return false; else return true;
    }

    //unsign a float
    public static float Unsigned(float Val)
    {
        if (Val < 0f) Val *= -1;
        return Val;
    }



}
