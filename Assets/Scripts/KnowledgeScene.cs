using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KnowledgeScene : MonoBehaviour {

    public GameObject interestPointMarker;

    public int rpValue;

    bool markerActive = false;
    GameObject player;

    void Start()
    {
        interestPointMarker.SetActive(false);
        interestPointMarker.GetComponent<Transform>();
        
    }

    void Update()
    {
       

        if (markerActive == true)
        {
          player = GameObject.FindGameObjectWithTag("Player");
          interestPointMarker.transform.LookAt(player.GetComponent<Transform>().transform);
            if (Input.GetKeyDown(KeyCode.E))
            {
                markerActive = false;
                Collect();
            }
        }
    }

	void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            interestPointMarker.SetActive(true);
            markerActive = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            interestPointMarker.SetActive(false);
            markerActive = false;
        }
    }

    void Collect()
    {
        PlayerResources.ReserchPoints += rpValue;
        Debug.Log("Collected");
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
