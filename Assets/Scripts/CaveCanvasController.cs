using UnityEngine;
using System.Collections;

public class CaveCanvasController : MonoBehaviour {

    public GameObject caveUI;
    public static bool caveCanvas = false;

    void Update()
    {
//        if(caveCanvas == true)
//        {
//            Cursor.lockState = CursorLockMode.None;
//            Cursor.visible = true;
//        }
//        else
//        {
//            Cursor.lockState = CursorLockMode.Locked;
//            Cursor.visible = false;
//        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            caveCanvas = true;
            caveUI.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            caveCanvas = false;
            caveUI.gameObject.SetActive(false);
        }
    }
}
