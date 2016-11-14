using UnityEngine;
using System.Collections;

public class PlayerResources : MonoBehaviour {

    public static int ReserchPoints;

    public static int food;
    public static int wood;
    public static int stone;
    public static int gold;

    

    void Start()
    {
        ReserchPoints = 1000;
        food = 10;
        wood = 0;
        stone = 0;
        gold = 0;
    }

   
}
