using UnityEngine;
using System.Collections;

public class TreeController : MonoBehaviour {

    public GameObject stickPrefab;
    int treeHealth = 10;
    public int treeWood = 80;
    GameObject tree;

    public int speed = 4;

    float spawntime;
    

	// Use this for initialization
	void Start () {

        StartCoroutine(spawnSticks());
        tree = this.gameObject;
        tree.GetComponent<Rigidbody>().isKinematic = true;
	
	}
	
	// Update is called once per frame
	void Update () {

        if(treeWood <= 20)
        {
            TreeFall();
        }

        if(treeWood <= 0)
        {
            StartCoroutine(TreeDeath());
        }
	
	}

    IEnumerator spawnSticks()
    {
        yield return new WaitForSeconds(spawntime);
        spawntime = Random.Range(100f, 500f);
        Instantiate(stickPrefab, new Vector3(this.transform.position.x + Random.Range(-7, 7), this.transform.position.y + 1, this.transform.position.z + Random.Range(-7, 7)), Quaternion.identity);
        StartCoroutine(spawnSticks());
    }

    void TreeFall()
    {
        tree.GetComponent<Rigidbody>().isKinematic = false;
        tree.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }

    IEnumerator TreeDeath()
    {
        yield return new WaitForSeconds(4);
        Destroy(tree);
    }
}
