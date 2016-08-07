using UnityEngine;
using System.Collections;

public class SwapToBreakable : MonoBehaviour {

    public GameObject Breakable;
    public GameObject Solid;

	// Use this for initialization
	void Start () {
        Solid.SetActive(true);
        Breakable.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (PlayerController.isAttacking == true)
        {
            Solid.SetActive(false);
            StartCoroutine(switchBuildType());
            
        }
    }

    void OnTriggerEnter()
    {
        
    }

    IEnumerator switchBuildType()
    {
        yield return new WaitForSeconds(0.01f);
            Breakable.SetActive(true);

    }
}
