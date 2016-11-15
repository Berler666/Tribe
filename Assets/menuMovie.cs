using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menuMovie : MonoBehaviour {

    public MovieTexture movTexture;

    void Start()
    {
        GetComponent<RawImage>().texture = movTexture as MovieTexture;
        movTexture.Play();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
