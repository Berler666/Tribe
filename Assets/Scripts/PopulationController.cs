using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopulationController : MonoBehaviour {

    public static int populationCount = 0;
    public int populationCountLmit = 10;
    public int populationCountMaxLmit = 100;

    public Text populationCounter;

	// Use this for initialization
	void Start () {

        populationCounter.GetComponent<Text>();
	
	}
	
	// Update is called once per frame
	void Update ()
    { 
            populationCounter.text = "Population: " + populationCount + " | " + populationCountLmit;
	}
}
