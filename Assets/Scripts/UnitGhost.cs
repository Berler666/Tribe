using UnityEngine;
using System.Collections;

public class UnitGhost : MonoBehaviour {

	
	void Update ()
    {
        transform.position = new Vector3(BuildMenu.GroundMousePoint.x, BuildMenu.GroundMousePoint.y + 0.5f, BuildMenu.GroundMousePoint.z);

        if(Input.GetMouseButtonUp(1))
        {
            Destroy(this.gameObject);
            BuildMenu.ghostActive = false;
        }
	}
}
