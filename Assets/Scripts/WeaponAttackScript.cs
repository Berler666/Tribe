using UnityEngine;
using System.Collections;

public class WeaponAttackScript : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () {
		

		anim=GetComponent<Animator>();
		anim.Play("PrimitiveClubIdleAttack");
	
	}
	
	// Update is called once per frame
	void Update () {



		if (Input.GetButton("Fire1"))

		{
			anim.Play("PrimitiveClubAttack");
		}



	
	}


}
