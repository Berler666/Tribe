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
        spawntime = Random.Range(1, 10);
        Instantiate(stickPrefab, new Vector3(this.transform.position.x + Random.Range(-20, 20), this.transform.position.y + 1, this.transform.position.z + Random.Range(-20, 20)), Quaternion.identity);
        StartCoroutine(spawnSticks());
    }
}
