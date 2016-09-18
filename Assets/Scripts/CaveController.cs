using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CaveController : MonoBehaviour {

    public Slider caveClaim;
    public GameObject claimedText;
    public GameObject caveClaimCanvas;
    public GameObject CaveCanvas;

    public GameObject homePanel;
    public GameObject statsPanel;

    public GameObject workBench;

    

   

    public bool canClaim = false;
    public bool caveClaimed = false;
   
    float time;
    float maxTime = 50;

	// Use this for initialization
	void Start () {

        
        caveClaimCanvas.SetActive(false);
        caveClaim.maxValue = maxTime;
        claimedText.SetActive(false);
	
	}
	
	// Update is called once per frame
	void Update () {
        


        if (canClaim == true)
        {
            if(Input.GetKey(KeyCode.E))
            {
                time += Time.deltaTime;

                if(time < maxTime)
                {
                    caveClaim.GetComponent<Slider>().value = Mathf.Lerp(0f, maxTime, time);
                   
                }

                 if(caveClaim.value == maxTime)
                {
                    Debug.Log("Cave Claimed");
                    claimedText.SetActive(true);
                    caveClaimed = true;
                    canClaim = false;
                    StartCoroutine(DestroyClaimBar());
                }
                
            } else if(!Input.GetKey(KeyCode.E) && caveClaim.value != maxTime)
            {
                caveClaim.value = 0;
                time = 0;
            }
       
        }
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && caveClaimed == false)
        {
            caveClaimCanvas.SetActive(true);
            canClaim = true;
        }

        if(caveClaimed == true)
        {
            OpenCaveMenu();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player" && caveClaimed == false)
        {
            caveClaimCanvas.SetActive(false);
            canClaim = false;
        }

        if (caveClaimed == true)
        {
            CloseCaveMenu();
        }
    }


    public IEnumerator DestroyClaimBar()
    {
        yield return new WaitForSeconds(3f);
        Destroy(caveClaimCanvas);
        workBench.SetActive(true);
    }

    public void OpenCaveMenu()
    {
        CaveCanvas.SetActive(true);
       
    }

    public void CloseCaveMenu()
    {
        CaveCanvas.SetActive(false);
       
    }

    public void HomePanel()
    {
        homePanel.SetActive(true);
        statsPanel.SetActive(false);
    }

    public void StatsPanel()
    {
        statsPanel.SetActive(true);
        homePanel.SetActive(false);
    }
}
