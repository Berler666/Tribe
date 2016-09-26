using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.RotateAround(new Vector3(500,0,500), Vector3.right, 2.4f * Time.deltaTime);
        transform.LookAt(new Vector3(500, 0, 500));
	
	}
}
