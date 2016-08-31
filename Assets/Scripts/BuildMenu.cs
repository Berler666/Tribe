using UnityEngine;
using System.Collections;

public class BuildMenu : MonoBehaviour {

    public GameObject ghost;
    public GameObject prefab;
    public Material ghostMaterial;
    public static bool ghostActive = false;

    public static Vector3 GroundMousePoint;
    public LayerMask GroundOnly;
    RaycastHit hit;

	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, GroundOnly))
        {
            GroundMousePoint = hit.point;

        }

     }

    public void buildObject1()
    {
        if(ghostActive)
        {
            Destroy(ghost);
        }
        ghost = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        ghostActive = true;
        
    }
}
