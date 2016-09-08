using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CaveController : MonoBehaviour {

    public Slider caveClaim;
    public GameObject caveCanvas;

    public bool canClaim = false;

	// Use this for initialization
	void Start () {

        
        caveCanvas.SetActive(false);
	
	}
	
	// Update is called once per frame
	void Update () {

        if(canClaim == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                caveClaim.GetComponent<Slider>().value += 0.1f;
            }
            else
            {
                caveClaim.GetComponent<Slider>().value = 0;
            }
        }
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            caveCanvas.SetActive(true);
            canClaim = true;


        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            caveCanvas.SetActive(false);
            canClaim = false;
        }
    }
}
