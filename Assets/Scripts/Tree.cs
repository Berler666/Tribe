using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {

    public GameObject stickPrefab;

    float spawntime;
    

	// Use this for initialization
	void Start () {

        StartCoroutine(spawnSticks());
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator spawnSticks()
    {
        yield return new WaitForSeconds(spawntime);
        spawntime = Random.Range(100f, 500f);
        Instantiate(stickPrefab, new Vector3(this.transform.position.x + Random.Range(-7, 7), this.transform.position.y + 1, this.transform.position.z + Random.Range(-7, 7)), Quaternion.identity);
        StartCoroutine(spawnSticks());
    }
}
