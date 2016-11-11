using UnityEngine;
using System.Collections;

public class devOptions : MonoBehaviour {

    GameObject daynighobject;
    DayNightCycle daynightScript;

    void Start()
    {
        daynighobject = GameObject.Find("Day/NightControler");
        daynightScript = daynighobject.GetComponent<DayNightCycle>();
    }

    public void SkipTilMorning()
    {
        daynightScript.time = 20000;
    }

    public void SkipTilDusk()
    {
        daynightScript.time = 50000;
    }

    public void SkipTilMidday()
    {
        daynightScript.time = 39000;
    }

    public void SkipTilMidNIght()
    {
        daynightScript.time = 0;
    }
}
