using UnityEngine;
using System.Collections;

public class UnitGhost : MonoBehaviour {

	
	void Update ()
    {
        transform.position = new Vector3(BuildMenu.GroundMousePoint.x, BuildMenu.GroundMousePoint.y + 0.5f, BuildMenu.GroundMousePoint.z);
        

        if(Input.GetMouseButtonUp(1))
        {
            BuildMenu.ghostActive = false;
            Destroy(this.gameObject);
        }

        ApplyRotation();
	}

    void ApplyRotation()
    {
        float deadZone = 0.01f;
        float ease = 10000f;

        float scrollWheelValue = Input.GetAxis("Mouse ScrollWheel") * ease * Time.deltaTime;

        if(scrollWheelValue > -deadZone && scrollWheelValue < deadZone || scrollWheelValue == 0f)
        {
            return;
        }

        this.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + scrollWheelValue, transform.eulerAngles.z);
    }
}
