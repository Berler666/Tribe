using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public GameObject[] weapons;

    public int currentWeapon = 0;

    



    void Update()
    {
        
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
            SwitchWeapon(0);
            }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(2);
        }



    }

    void SwitchWeapon(int index)
    {
        currentWeapon = index;
        for(int i = 0; i < weapons.Length; i++)
        {
            if(i == index)
            {
                weapons[i].gameObject.SetActive(true);
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }
        }
    }
}
