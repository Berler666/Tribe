using UnityEngine;
using System.Collections;

public class GhostColisonTriggers : MonoBehaviour {

    void Start()
    {
        BuildMenu.PassedTriggerTest = true;
    }

	void OnTriggerEnter(Collider col)
    {
        if(col.transform.gameObject.layer == LayerMask.NameToLayer("Units"))
        {
            BuildMenu.PassedTriggerTest = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.gameObject.layer == LayerMask.NameToLayer("Units"))
        {
            BuildMenu.PassedTriggerTest = true;
        }
    }
}
